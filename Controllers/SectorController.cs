using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Models;
using b8vB6mN3zAe.Tools;
using static b8vB6mN3zAe.Tools.Utils;
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
        [Route("id")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetSectorByID([FromHeader] string id)
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

                var sector = await _context.Sectors
                .Include(sector => sector.Lab)
                .Include(sector => sector.Cities)
                .Include(sector => sector.Lab.City)
                .Include(sector => sector.Users)
                .FirstOrDefaultAsync(sector => sector.ID == id);

                if (sector is null)
                {
                    return NotFound("Sector not Found.");
                }


                if (!UserHaveAccess(sector.Users, accessUserId, _context))
                {
                    return Unauthorized("Invalid access token.");
                }

                return Ok(sector.ToSectorResponseDto());

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetSectors()
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

                var sectors = await _context.Sectors
                .Include(sector => sector.Lab)
                .Include(sector => sector.Cities)
                .Include(sector => sector.Lab.City)
                .Include(sector => sector.Users)
                .ToArrayAsync();


                var accessibleSectors = sectors
                    .Where(sector => Utils.UserHaveAccess(sector.Users, accessUserId, _context))
                    .Select(sector => sector.ToSectorResponseDto())
                    .ToArray();


                return Ok(accessibleSectors);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSector(CreateSectorRequest sectorRequest)
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


                //get sector from db 
                var dbSector = await _context.Sectors.FindAsync(id);

                if (dbSector is null)
                {
                    return NotFound("Sector Not found.");
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