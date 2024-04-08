using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace b8vB6mN3zAe.Controllers
{
    [Route("user-sector")]
    [ApiController]
    public class UserSectorController : ControllerBase
    {

        private readonly ApplicationDBContext _context;

        public UserSectorController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AffectSectorToUser(CreateUserSectorRequest userSectorRequest)
        {
            try
            {

                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //check if user exist
                var dbUser = await _context.Users.FindAsync(userSectorRequest.UserID);
                if (dbUser is null)
                {
                    return NotFound("User not found.");
                }

                //check sector if exist
                var sector = await _context.Sectors.FindAsync(userSectorRequest.SectorID);
                if (sector is null)
                {
                    return NotFound("Sector not found.");
                }

                //create relation
                await _context.UserSector.AddAsync(userSectorRequest.FromCreateUserSectorRequestDto());
                await _context.SaveChangesAsync();

                return Created();

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error.");
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserSectorRelation([FromHeader] int id)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }


                //get city from db 
                var dbRelation = await _context.UserSector.FindAsync(id);

                if (dbRelation is null)
                {
                    return NotFound("Relation Not found");
                }
                _context.UserSector.Remove(dbRelation);
                await _context.SaveChangesAsync();

                return Ok("Relation Deleted.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }


    }
}