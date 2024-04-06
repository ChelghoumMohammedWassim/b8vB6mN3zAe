using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace b8vB6mN3zAe.Controllers
{
    [Route("sector")]
    [ApiController]
    public class SectorController : ControllerBase
    {

        private readonly ApplicationDBContext _context;

        public SectorController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetSector()
        {
            try
            {
                var sectors = await _context.Sectors
                .Include(city => city.Lab)
                .Include(city => city.Cities)
                .Include(city => city.Lab.City)
                .Select(city => city.ToSectorResponseDto())
                .ToArrayAsync();

                return Ok(sectors);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost]
        [Route("/create-sector")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateCity(CreateSectorRequest sectorRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //check if all ready exist
                var usingNameSector = await _context.Sectors.FirstOrDefaultAsync(city => city.Name == sectorRequest.Name);
                if (usingNameSector is not null)
                {
                    return Conflict("City already exist.");
                }

                //check sector if exist 
                var lab = await _context.Sectors.FindAsync(sectorRequest.LabID);
                if (lab is null)
                {
                    return NotFound("Lab not exist.");
                }

                await _context.Sectors.AddAsync(sectorRequest.FromCreateSectorRequestDto());

                await _context.SaveChangesAsync();

                return Created();

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPut]
        [Route("/update-sector")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSector(UpdateSectorRequest sectorRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //get sector from db
                var dbSector = await _context.Sectors.FirstOrDefaultAsync(se => se.ID == sectorRequest.ID);
                if (dbSector is null)
                {
                    return NotFound("Sector not exist." + sectorRequest.ID);
                }

                //check if all ready exist
                var usingNameSector = await _context.Sectors.FirstOrDefaultAsync(sector => sector.Name == sectorRequest.Name);
                if (usingNameSector is not null && usingNameSector.ID != sectorRequest.ID)
                {
                    return Conflict("Sector already exist.");
                }

                //check sector if exist 
                if (sectorRequest.LabID != null)
                {
                    var lab = await _context.Labs.FindAsync(sectorRequest.LabID);
                    if (lab is null)
                    {
                        return NotFound("lab not exist.");
                    }
                }


                dbSector.Name = sectorRequest.Name;
                dbSector.LabID = sectorRequest.LabID;

                await _context.SaveChangesAsync();

                return Ok("Sector updated.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSector([FromHeader] String id)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }


                //get city from db 
                var dbSector = await _context.Sectors.FindAsync(id);

                if (dbSector is null)
                {
                    return NotFound("Sector Not found");
                }
                _context.Sectors.Remove(dbSector);
                await _context.SaveChangesAsync();

                return Ok("Sector Deleted.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }


    }
}