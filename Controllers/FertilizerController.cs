using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace b8vB6mN3zAe.Controllers
{
    [Route("fertilizer")]
    [ApiController]
    public class FertilizerController : ControllerBase
    {

        private readonly ApplicationDBContext _context;

        public FertilizerController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
        }


        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetFertilizers()
        {
            try
            {
                //get fertilizer from db
                var fertilizers = await _context.Fertilizers.ToArrayAsync();

                return Ok(fertilizers.Select(fertilizer => fertilizer.ToFertilizerResponseDto()));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }

        
        [HttpGet]
        [Route("id")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetFertilizerBy([FromHeader] string id)
        {
            try
            {
                //get cities from db
                var fertilizer = await _context.Fertilizers.
                            FirstOrDefaultAsync(fertilizer => fertilizer.ID == id);


                if (fertilizer is null)
                {
                    return NotFound("Fertilizer not Found");
                }


                return Ok(fertilizer.ToFertilizerResponseDto());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateFertilizer(FertilizerCreateRequest fertilizerRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //check if all ready exist
                var usingNameFertilizer = await _context.Fertilizers.FirstOrDefaultAsync(fertilizer => fertilizer.Name.ToLower() == fertilizerRequest.Name.ToLower());
                if (usingNameFertilizer is not null)
                {
                    return Conflict("Fertilizer already exist.");
                }


                await _context.Fertilizers.AddAsync(fertilizerRequest.FromCreateFertilizerRequestDto());

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
        public async Task<IActionResult> UpdateFertilizer(FertilizerUpdateRequest fertilizerRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //check if all ready exist
                var usingNameFertilizer = await _context.Fertilizers.FirstOrDefaultAsync(fertilizer => fertilizer.Name.ToLower() == fertilizerRequest.Name.ToLower());
                if (usingNameFertilizer is not null && usingNameFertilizer.ID != fertilizerRequest.ID)
                {
                    return Conflict("Fertilizer already exist.");
                }

                //get fertilizer from db 
                var dbFertilizer = await _context.Fertilizers.FindAsync(fertilizerRequest.ID);

                if (dbFertilizer is null)
                {
                    return NotFound("Fertilizer Not found.");
                }


                dbFertilizer.Name = fertilizerRequest.Name;
                dbFertilizer.N = fertilizerRequest.N;
                dbFertilizer.Ammoniacal = fertilizerRequest.Ammoniacal;
                dbFertilizer.Ureic = fertilizerRequest.Ureic;
                dbFertilizer.Nitric = fertilizerRequest.Nitric;
                dbFertilizer.P2O5 = fertilizerRequest.P2O5;
                dbFertilizer.K2O = fertilizerRequest.K2O;
                dbFertilizer.MgO = fertilizerRequest.MgO;
                dbFertilizer.CaO = fertilizerRequest.CaO;
                dbFertilizer.Fe = fertilizerRequest.Fe;
                dbFertilizer.Zn = fertilizerRequest.Zn;
                dbFertilizer.Mn = fertilizerRequest.Mn;
                dbFertilizer.S = fertilizerRequest.S;
                dbFertilizer.Cl = fertilizerRequest.Cl;
                dbFertilizer.Density = fertilizerRequest.Density;
                dbFertilizer.Solubility = fertilizerRequest.Solubility;
                dbFertilizer.ConductivityMax = fertilizerRequest.ConductivityMax;
                dbFertilizer.ReactionType = fertilizerRequest.ReactionType;
                dbFertilizer.FertilizerType = fertilizerRequest.FertilizerType;
                dbFertilizer.SubType = fertilizerRequest.SubType;

                await _context.SaveChangesAsync();

                return Ok("Fertilizer updated.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteFertilizer([FromHeader] string id)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }


                //get fertilizer from db 
                var dbFertilizer = await _context.Fertilizers.FindAsync(id);

                if (dbFertilizer is null)
                {
                    return NotFound("Fertilizer Not found.");
                }

                _context.Fertilizers.Remove(dbFertilizer);
                await _context.SaveChangesAsync();

                return Ok("Fertilizer Deleted.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }

    }
}