using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace b8vB6mN3zAe.Controllers
{
    [Route("city")]
    [ApiController]
    public class CityController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly String _SECRETKEY;

        public CityController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _SECRETKEY = configuration["MySecretKey"];
        }


        [HttpGet]
        [Route("all")]
        [Authorize]
        public async Task<IActionResult> GetCities()
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


                //get cities from db
                var cities = await _context.Cities.
                            Include(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(city => city.ZipCodes).
                            OrderBy(city => city.ID).
                            ToArrayAsync();

                // Filter the cities based on user access 
                var accessibleCities = cities
                    .Where(city => Utils.UserHaveAccess(city?.Sector?.Users, accessUserId, _context))
                    .Select(city => city.ToCityResponseDto())
                    .ToArray();

                return Ok(accessibleCities);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }

        [HttpGet]
        [Route("id")]
        [Authorize]
        public async Task<IActionResult> GetCityBy([FromHeader] int id)
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

                //get cities from db
                var city = await _context.Cities.
                            Include(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(city => city.ZipCodes).
                            Include(city => city.ZipCodes).
                            FirstOrDefaultAsync(city => city.ID == id);


                if (city is null)
                {
                    return NotFound("city not Found");
                }

                if (!Utils.UserHaveAccess(city.Sector.Users, accessUserId, _context))
                {
                    return Unauthorized("Invalid access token.");
                }

                return Ok(city.ToCityResponseDto());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCity(UpdateCityRequest cityRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //check if all ready exist
                var usingNameCity = await _context.Cities.FirstOrDefaultAsync(city => city.Name.ToLower() == cityRequest.Name.ToLower());
                if (usingNameCity is not null && usingNameCity.ID != cityRequest.ID)
                {
                    return Conflict("City already exist.");
                }

                //get city from db 
                var dbCity = await _context.Cities.FindAsync(cityRequest.ID);

                if (dbCity is null)
                {
                    return NotFound("City Not found.");
                }

                //check sector if exist 
                var sector = await _context.Sectors.FindAsync(cityRequest.SectorID);
                if (sector is null)
                {
                    return NotFound("Sector not exist.");
                }

                dbCity.Name = cityRequest.Name;
                dbCity.SectorID = cityRequest.SectorID;

                await _context.SaveChangesAsync();

                return Ok("City updated.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCity(CreateCityRequest cityRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //check if all ready exist
                var usingNameCity = await _context.Cities.FirstOrDefaultAsync(city => city.Name.ToLower() == cityRequest.Name.ToLower());
                if (usingNameCity is not null)
                {
                    return Conflict("City already exist.");
                }

                //check sector if exist 
                if (cityRequest.SectorID != null)
                {
                    var sector = await _context.Sectors.FindAsync(cityRequest.SectorID);
                    if (sector is null)
                    {
                        return NotFound("Sector not exist.");
                    }
                }


                await _context.Cities.AddAsync(cityRequest.FromCreateCityRequestDto());

                await _context.SaveChangesAsync();

                return Created();

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCity([FromHeader] int id)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }


                //get city from db 
                var dbCity = await _context.Cities.FindAsync(id);

                if (dbCity is null)
                {
                    return NotFound("City Not found.");
                }
                _context.Cities.Remove(dbCity);
                await _context.SaveChangesAsync();

                return Ok("City Deleted.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }

    }
}