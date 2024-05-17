using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Models;
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

        public LandController(ApplicationDBContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("all")]
        [Authorize]
        public async Task<IActionResult> GetLands()
        {
            try
            {
                var lands = await _context.Lands.
                Include(land => land.Farmer).
                Include(land => land.Exploitations)
                .Select(land => land.ToLandListResponseDto()).ToListAsync();

                return Ok(lands);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }

        [HttpGet]
        [Route("id")]
        [Authorize(Roles = "Agronomist, Pedologist, Admin")]
        public async Task<IActionResult> GetLandByID([FromHeader] String id)
        {
            try
            {
                var lands = await _context.Lands.
                Include(land => land.Farmer).
                Include(land => land.Exploitations).
                FirstOrDefaultAsync(land => land!.ID == id);

                if (lands is null)
                {
                    return NotFound("Land Not found.");
                }

                return Ok(lands.ToLandListResponseDto());

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateLand(LandUpdateRequest landRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                var dbLand = await _context.Lands.FirstOrDefaultAsync(land=> land.ID == landRequest.ID);
                if  (dbLand is null)
                {
                    return NotFound("Land Not Found.");
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