using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Dtos.UserDtos;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Models;
using InsuranceAPI.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace b8vB6mN3zAe.Controllers
{
    [Route("User")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly IConfiguration _configuration;

        public UserController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [HttpGet, Authorize]
        public IActionResult test()
        {
            return Ok("fuck you");
        }


        [HttpPost]
        [Route("/login")]
        public async Task<IActionResult> Login(LoginUserRequest userRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //get user from db
                User? dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userRequest.UserName);

                //user doesn't exist
                if (dbUser is null)
                {
                    return Unauthorized("Bad user information");
                }

                //check password
                if (!BCrypt.Net.BCrypt.Verify(userRequest.Password, dbUser.Password))
                {
                    return Unauthorized("Bad user information");
                }


                //create token
                String token = Token.CreateToken(dbUser, _configuration["MySecretKey"]);

                return Ok(token);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPost]
        [Route("/register")]
        public async Task<IActionResult> Register(CreateUserRequest userRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //check if user name already used 
                User? dbUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userRequest.UserName);
                if (dbUser is not null)
                {
                    return Unauthorized("User name can't be used.");
                }

                //add user to db
                await _context.Users.AddAsync(userRequest.FromCreateUserRequestDto());
                await _context.SaveChangesAsync();

                return Created();
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }

    }
}