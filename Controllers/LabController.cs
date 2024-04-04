using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Dtos.LabDtos;
using b8vB6mN3zAe.Dtos.UserDtos;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Models;
using b8vB6mN3zAe.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace b8vB6mN3zAe.Controllers
{
    [Route("Lab")]
    [ApiController]
    public class LabController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly String _SECRETKEY;

        public LabController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _SECRETKEY = configuration["MySecretKey"];
        }


        [HttpGet]
        [Route("/lab-by-token")]
        [Authorize(Roles = "Lab")]
        public async Task<IActionResult> GetUserInformationWithToken()
        {
            try
            {
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                //identify Lab
                String requestLabID = Token.DecodeToken(accessToken, _SECRETKEY);
                if (requestLabID is null)
                {
                    return Unauthorized("Invalid Authorization.");
                }

                //get lab from db
                Lab? dbLab = await _context.Labs.Include(lab=> lab.City).SingleOrDefaultAsync(lab => lab.ID == requestLabID);
                if (dbLab is null)
                {
                    return NotFound("User Not Found.");
                }

                return Ok(dbLab.ToLabResponseDto());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }

        [HttpGet]
        [Route("/all-labs")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllLabForAdmin()
        {
            try
            {
                //get user auth 
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");

                //get user id
                String requestUserID = Token.DecodeToken(accessToken, _SECRETKEY);
                if (requestUserID is null)
                {
                    return Unauthorized("Invalid Authorization.");
                }
                //select labs 
                var labs = await _context.Labs
                .Include(lab=> lab.City)
                .Select(lab => lab.ToAdminLabsListResponseDto())
                .ToListAsync();

                return Ok(labs);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpPost]
        [Route("/lab-login")]
        public async Task<IActionResult> Login(LoginRequest labRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //get lab from db
                Lab? dbLab = await _context.Labs.FirstOrDefaultAsync(u => u.UserName == labRequest.UserName);

                //user doesn't exist
                if (dbLab is null)
                {
                    return Unauthorized("Bad user information");
                }

                //check password
                if (!BCrypt.Net.BCrypt.Verify(labRequest.Password, dbLab.Password))
                {
                    return Unauthorized("Bad user information");
                }

                //check if the account is active
                if (!dbLab.IsActive)
                {
                    return Unauthorized("Account Deactivated.");
                }

                //create token
                String token = Token.CreateToken(dbLab, _SECRETKEY);

                return Ok(token);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPost]
        [Route("/lab-register"), Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(CreateLabRequest labRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //check if user name already used 
                Lab? dbLab = await _context.Labs.FirstOrDefaultAsync(u => u.UserName == labRequest.UserName);
                if (dbLab is not null)
                {
                    return Unauthorized("User name can't be used.");
                }

                //check city if exist
                City? city= await _context.Cities.FindAsync(labRequest.City);
                if(city is null)
                {
                    return NotFound("City not found.");
                }

                //add user to db
                await _context.Labs.AddAsync(labRequest.FromCreateLabRequestDto());
                await _context.SaveChangesAsync();

                return Created();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }


        [HttpPut]
        [Route("/update-lab-by-token")]
        [Authorize(Roles = "Lab")]
        public async Task<IActionResult> UpdateUserByToke(UpdateLabRequest labRequest)
        {
            try
            {
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");

                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //identify user
                String requestLabID = Token.DecodeToken(accessToken, _SECRETKEY);
                if (requestLabID is null)
                {
                    return Unauthorized("Invalid Authorization.");
                }

                //get user from db
                Lab? dbLab = await _context.Labs.FindAsync(requestLabID);
                if (dbLab is null)
                {
                    return NotFound("User Not found to be updated.");
                }

                //check city if exist
                City? city= await _context.Cities.FindAsync(labRequest.City);
                if(city is null)
                {
                    return NotFound("City not found.");
                }

                //check confirmation password
                if (!BCrypt.Net.BCrypt.Verify(labRequest.CurrentPassword, dbLab.Password))
                {
                    return Unauthorized("Wrong password Confirmation");
                }

                //check if user name already used 
                Lab? userNameUsageCheck = await _context.Labs.FirstOrDefaultAsync(u => u.UserName == labRequest.UserName);
                if (userNameUsageCheck is not null && userNameUsageCheck.ID != dbLab.ID)
                {
                    return Unauthorized("User name can't be used.");
                }

                //update user
                dbLab.Name = labRequest.Name;
                dbLab.Address = labRequest.Address;
                dbLab.PhoneNumber = labRequest.PhoneNumber;
                dbLab.Email = labRequest.Email;
                dbLab.CityID = labRequest.City;
                dbLab.UserName = labRequest.UserName;
                if (labRequest.NewPassword is not null)
                {
                    dbLab.Password = BCrypt.Net.BCrypt.HashPassword(labRequest.NewPassword);
                }
                
                await _context.SaveChangesAsync();

                return Ok("Lab Updated.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPut]
        [Route("/update-lab-by-id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserByID(AdminUpdateLabRequest labRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //identify user
                Lab? dbLab = await _context.Labs.FindAsync(labRequest.ID);
                if (dbLab is null)
                {
                    return NotFound("User Not found to be updated.");
                }

                //check city if exist
                City? city= await _context.Cities.FindAsync(labRequest.City);
                if(city is null)
                {
                    return NotFound("City not found.");
                }

                //update user
                dbLab.Name = labRequest.Name;
                dbLab.Address = labRequest.Address;
                dbLab.PhoneNumber = labRequest.PhoneNumber;
                dbLab.Email = labRequest.Email;
                dbLab.CityID = labRequest.City;
                if (labRequest.Password is not null)
                {
                    dbLab.Password = BCrypt.Net.BCrypt.HashPassword(labRequest.Password);
                }

                await _context.SaveChangesAsync();

                return Ok("Lab Updated.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

    }
}