using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Models;
using b8vB6mN3zAe.Tools;
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
        private readonly String _SECRETKEY;

        public SectorController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _SECRETKEY = configuration["MySecretKey"];
        }


        [HttpGet]
        [Route("all")]
        [Authorize]
        public async Task<IActionResult> GetAllSector()
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

        [HttpGet]
        [Route("id")]
        [Authorize]
        public async Task<IActionResult> GetSectorByID([FromHeader] string id)
        {
            try
            {
                var sector = await _context.Sectors
                .Include(city => city.Lab)
                .Include(city => city.Cities)
                .Include(city => city.Lab.City)
                .FirstOrDefaultAsync(city => city.ID == id);

                if (sector is null)
                {
                    return NotFound("Sector not Found");
                }

                return Ok(sector.ToSectorResponseDto());

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpGet]
        [Route("token")]
        [Authorize]
        public async Task<IActionResult> GetSectorByUserToken()
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);
                if(accessUserId is null)
                {
                    return Unauthorized("Invalid token");
                }

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
        [Authorize]
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
                var usingNameSector = await _context.Sectors.FirstOrDefaultAsync(sector => sector.Name.ToLower() == sectorRequest.Name.ToLower());
                if (usingNameSector is not null)
                {
                    return Conflict("Sector already exist.");
                }

                //check lab if exist 
                if (sectorRequest.LabID != null)
                {
                    var lab = await _context.Labs.FindAsync(sectorRequest.LabID);
                    if (lab is null)
                    {
                        return NotFound("lab not exist.");
                    }
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
        [Authorize]
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
                var usingNameSector = await _context.Sectors.FirstOrDefaultAsync(sector => sector.Name.ToLower() == sectorRequest.Name.ToLower());
                if (usingNameSector is not null && usingNameSector.ID != sectorRequest.ID)
                {
                    return Conflict("Sector already exist.");
                }

                //check lab if exist 
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
        [Authorize]
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