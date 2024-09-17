using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Models;
using b8vB6mN3zAe.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace b8vB6mN3zAe.Controllers
{
    [Route("land")]
    [ApiController]
    public class LandController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly String _SECRETKEY;

        public LandController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _SECRETKEY = configuration["MySecretKey"];
        }


        [HttpGet]
        [Route("all")]
        [Authorize]
        public async Task<IActionResult> GetLands()
        {
            try
            {

                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                if (accessUserId is null)
                {
                    return Unauthorized("Invalid token.");
                }

                var lands = await _context.Lands.
                Include(land => land.Exploitations)
                .Include(land => land.Farmer)
                .ThenInclude(farmer => farmer.ZipCode)
                .ThenInclude(zipCode => zipCode.City)
                .ThenInclude(city => city.Sector)
                .ThenInclude(sector => sector.Users).ToListAsync();

                var accessibleLands = lands.
                        Where(land => Utils.UserHaveAccess(land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context)).
                        Select(land => land.ToLandListResponseDto());

                return Ok(accessibleLands);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }

        [HttpGet]
        [Route("id")]
        [Authorize]
        public async Task<IActionResult> GetLandByID([FromHeader] String id)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                var lands = await _context.Lands
                .Include(land => land.Farmer)
                .ThenInclude(farmer => farmer.ZipCode)
                .ThenInclude(zipCode => zipCode.City)
                .ThenInclude(city => city.Sector).
                Include(land => land.Exploitations).
                FirstOrDefaultAsync(land => land!.ID == id);

                if (lands is null)
                {
                    return NotFound("Land Not found.");
                }

                if (!Utils.UserHaveAccess(lands?.Farmer?.ZipCode?.City?.Sector?.Users, accessToken, _context))
                {
                    return Unauthorized("Invalid access token.");
                }

                return Ok(lands.ToLandListResponseDto());

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }



        [HttpPost]
        [Authorize(Roles = "Agronomist, Pedologist")]
        public async Task<IActionResult> CreateLand(LandCreateRequest landRequest)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }
                //check request farmer if exist 
                var dbFarmer = await _context.Farmers.FindAsync(landRequest.FarmerID);
                if (dbFarmer is null)
                {
                    return NotFound("Selected Farmer not found.");
                }

                // Check if a land with the same name already exists
                var dbLand = await _context.Lands
                    .FirstOrDefaultAsync(land => land.Name == landRequest.Name);


                Land newLand = landRequest.FromCreateLandRequestDto();
                //add land to db
                await _context.Lands.AddAsync(newLand);

                await _context.SaveChangesAsync();

                return Created();

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPut]
        [Authorize(Roles = "Agronomist, Pedologist")]
        public async Task<IActionResult> UpdateLand(LandUpdateRequest landRequest)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                var dbLand = await _context.Lands
                    .Include(land => land.Farmer)
                    .ThenInclude(farmer => farmer.ZipCode)
                    .ThenInclude(zipCode => zipCode.City)
                    .ThenInclude(city => city.Sector).
                    Include(land => land.Exploitations).
                    FirstOrDefaultAsync(land => land!.ID == landRequest.ID);


                if (dbLand is null)
                {
                    return NotFound("Land Not Found.");
                }


                if (!Utils.UserHaveAccess(dbLand?.Farmer?.ZipCode?.City?.Sector?.Users, accessToken, _context))
                {
                    return Unauthorized("Invalid access token.");
                }

                //check request farmer if exist 
                var dbFarmer = await _context.Farmers.FindAsync(landRequest.FarmerID);
                if (dbFarmer is null)
                {
                    return NotFound("Selected Farmer not found.");
                }

                //update the land
                dbLand.Name = landRequest.Name;
                dbLand.Rainfall = landRequest.Rainfall;

                await _context.SaveChangesAsync();

                return Ok("Land Updated.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

    }


}