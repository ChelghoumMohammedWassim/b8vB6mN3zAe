using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Dtos.UserDtos;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Models;
using b8vB6mN3zAe.Models.Enums;
using b8vB6mN3zAe.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace b8vB6mN3zAe.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly String _SECRETKEY;

        public UserController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _SECRETKEY = configuration["MySecretKey"];

            //insert default values
            DefaultValue.DefaultValue.InsertDefaultValues(_context);

        }


        [HttpGet]
        [Route("token")]
        [Authorize]
        public async Task<IActionResult> GetUserInformationWithToken()
        {
            try
            {
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                //identify user
                String requestUserID = Token.DecodeToken(accessToken, _SECRETKEY);
                if (requestUserID is null)
                {
                    return Unauthorized("Invalid Authorization.");
                }

                //get user from db
                User? dbUser = await _context.Users.Include(user => user.City).SingleOrDefaultAsync(u => u.ID == requestUserID);

                if (dbUser is null)
                {
                    return NotFound("User Not Found.");
                }

                return Ok(dbUser.ToUserResponseDto());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }

        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllForAdmin()
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
                //select users 
                var users = await _context.Users.Where(user => user.ID != requestUserID)
                .Include(u => u.City)
                .Include(u=> u.UsersSectors)
                .ThenInclude(userSector=> userSector.Sector)
                .OrderBy(user => user.Role).Reverse()
                .Select(user => user.ToAdminUsersListResponseDto())
                .ToListAsync();

                return Ok(users);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server error." + e.Message);
            }
        }

        [HttpGet]
        [Route("id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserByID([FromHeader] string id)
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
                //select users 
                var user = await _context.Users.Where(user => user.ID != requestUserID)
                .Include(u => u.City)
                .Include(u=> u.UsersSectors)
                .ThenInclude(userSector=> userSector.Sector)
                .FirstOrDefaultAsync(user => user.ID == id);

                if (user is null)
                {
                    return NotFound("User not Found.");
                }

                return Ok(user.ToAdminUsersListResponseDto());
            }
            catch (Exception e)
            {
                return StatusCode(500, "Internal Server error." + e.Message);
            }
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginRequest userRequest)
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
                    return Unauthorized("Bad user information.");
                }

                //check password
                if (!BCrypt.Net.BCrypt.Verify(userRequest.Password, dbUser.Password))
                {
                    return Unauthorized("Bad user information.");
                }

                //check if the account is active
                if (!dbUser.IsActive)
                {
                    return Unauthorized("Account Deactivated.");
                }

                //create token
                String token = Token.CreateToken(dbUser, _SECRETKEY);

                return Ok(token);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPost]
        [Route("register")]
        [Authorize(Roles = "Admin")]
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

                //check city if exist
                City? city = await _context.Cities.FindAsync(userRequest.City);
                if (city is null)
                {
                    return NotFound("City not found.");
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


        [HttpPut]
        [Route("token")]
        [Authorize]
        public async Task<IActionResult> UpdateUserByToke(UpdateUserRequest userRequest)
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
                String requestUserID = Token.DecodeToken(accessToken, _SECRETKEY);
                if (requestUserID is null)
                {
                    return Unauthorized("Invalid Authorization.");
                }

                //get user from db
                User? dbUser = await _context.Users.FindAsync(requestUserID);
                if (dbUser is null)
                {
                    return NotFound("User Not found to be updated.");
                }

                //check city if exist
                City? city = await _context.Cities.FindAsync(userRequest.City);
                if (city is null)
                {
                    return NotFound("City not found.");
                }

                //check confirmation password
                if (!BCrypt.Net.BCrypt.Verify(userRequest.CurrentPassword, dbUser.Password))
                {
                    return Unauthorized("Wrong password Confirmation");
                }

                //check if user name already used 
                User? userNameUsageCheck = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userRequest.UserName);
                if (userNameUsageCheck is not null && userNameUsageCheck.ID != dbUser.ID)
                {
                    return Unauthorized("User name can't be used.");
                }

                //update user
                dbUser.FirstName = userRequest.FirstName;
                dbUser.LastName = userRequest.LastName;
                dbUser.Address = userRequest.Address;
                dbUser.PhoneNumber = userRequest.PhoneNumber;
                dbUser.Email = userRequest.Email;
                dbUser.CityID = userRequest.City;
                dbUser.UserName = userRequest.UserName;
                if (userRequest.NewPassword is not null)
                {
                    dbUser.Password = BCrypt.Net.BCrypt.HashPassword(userRequest.NewPassword);
                }

                await _context.SaveChangesAsync();

                return Ok("User Updated.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPut]
        [Route("id")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserByID(AdminUpdateUserRequest userRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //identify user
                User? dbUser = await _context.Users.FindAsync(userRequest.ID);
                if (dbUser is null)
                {
                    return NotFound("User Not found to be updated.");
                }
                // check user role
                if (dbUser.Role == Role.Admin)
                {
                    return Unauthorized("Can't update this user.");
                }

                //check city if exist
                City? city = await _context.Cities.FindAsync(userRequest.City);
                if (city is null)
                {
                    return NotFound("City not found.");
                }

                //update user
                dbUser.FirstName = userRequest.FirstName;
                dbUser.LastName = userRequest.LastName;
                dbUser.Address = userRequest.Address;
                dbUser.PhoneNumber = userRequest.PhoneNumber;
                dbUser.Email = userRequest.Email;
                dbUser.CityID = userRequest.City;
                if (userRequest.Password is not null)
                {
                    dbUser.Password = BCrypt.Net.BCrypt.HashPassword(userRequest.Password);
                }

                await _context.SaveChangesAsync();

                return Ok("User Updated.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

    }
}