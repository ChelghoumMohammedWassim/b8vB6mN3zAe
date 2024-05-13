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
                Include(land => land.Positions)
                .Select(land => land.ToLandResponseDto()).ToListAsync();

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
                Include(land => land.Positions).
                FirstOrDefaultAsync(land => land!.ID == id);

                if (lands is null)
                {
                    return NotFound("Land Not found.");
                }

                return Ok(lands.ToLandResponseDto());

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
                    .Include(land => land.Positions)
                    .FirstOrDefaultAsync(land => land.Name == landRequest.Name);

                if (dbLand is not null)
                {
                    var isSamePosition = dbLand.Positions.Count == landRequest.Positions.Count &&
                        dbLand.Positions.All(pos =>
                            landRequest.Positions.Any(reqPos =>
                                reqPos.longitude == pos.longitude && reqPos.latitude == pos.latitude));

                    if (isSamePosition)
                    {
                        return Conflict("A land with the same name and positions already exists.");
                    }
                }


                Land newLand = landRequest.FromCreateLandRequestDto();
                //add land to db
                await _context.Lands.AddAsync(newLand);

                //add potions
                foreach (PositionCreateRequest p in landRequest.Positions)
                {
                    await _context.Positions.AddAsync(p.FromCreatePositionRequestDto(newLand.ID));
                }

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

                var dbLand = await _context.Lands.Include(land=> land.Positions).FirstOrDefaultAsync(land=> land.ID == landRequest.ID);
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

                // Check if a land with the same name already exists
                var UsingNameOrPositionsLand = await _context.Lands
                    .Include(land => land.Positions)
                    .FirstOrDefaultAsync(land => land.Name == landRequest.Name && land.ID != landRequest.ID);

                if (UsingNameOrPositionsLand is not null)
                {
                    var isSamePosition = UsingNameOrPositionsLand.Positions.Count == landRequest.Positions.Count &&
                        UsingNameOrPositionsLand.Positions.All(pos =>
                            landRequest.Positions.Any(reqPos =>
                                reqPos.longitude == pos.longitude && reqPos.latitude == pos.latitude));

                    if (isSamePosition)
                    {
                        return Conflict("A land with the same name and positions already exists.");
                    }
                }

                //delete old positions
                foreach (Position p in dbLand.Positions)
                {
                    _context.Positions.Remove(p);
                }

                //update the land
                dbLand.Name = landRequest.Name;
                dbLand.Rainfall = landRequest.Rainfall;

                //add new potions
                foreach (PositionCreateRequest p in landRequest.Positions)
                {
                    await _context.Positions.AddAsync(p.FromCreatePositionRequestDto(landRequest.ID));
                }

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