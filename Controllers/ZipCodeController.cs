using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace b8vB6mN3zAe.Controllers
{
    [Route("zip-code")]
    [ApiController]
    public class ZipCodeController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly String _SECRETKEY;

        public ZipCodeController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _SECRETKEY = configuration["MySecretKey"];
        }


        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetZipCodes()
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //get cities from db
                var zipCodes = await _context.ZipCodes.
                            Include(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            ToArrayAsync();

                var accessibleZipCode = zipCodes.
                    Where(zipCode => Utils.UserHaveAccess(zipCode?.City?.Sector?.Users, accessUserId, _context)).
                    Select(zipCode => zipCode.ToZipCodeResponseDto());

                return Ok(accessibleZipCode);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("city")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetZipCodesByCity([FromHeader] int cityID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //get cities from db
                var zipCodes = await _context.ZipCodes.
                            Include(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            ToArrayAsync();

                var accessibleZipCode = zipCodes.
                    Where(zipCode => Utils.UserHaveAccess(zipCode?.City?.Sector?.Users, accessUserId, _context) && zipCode.CityID==cityID).
                    Select(zipCode => zipCode.ToZipCodeResponseDto());

                return Ok(accessibleZipCode);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("sector")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetZipCodesBySector([FromHeader] string sectorID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //get cities from db
                var zipCodes = await _context.ZipCodes.
                            Include(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            ToArrayAsync();

                var accessibleZipCode = zipCodes.
                    Where(zipCode => Utils.UserHaveAccess(zipCode?.City?.Sector?.Users, accessUserId, _context) && zipCode.City.SectorID==sectorID).
                    Select(zipCode => zipCode.ToZipCodeResponseDto());

                return Ok(accessibleZipCode);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("id")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetZipCodeByID([FromHeader] String id)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //get cities from db
                var zipCode = await _context.ZipCodes.
                            Include(zipCode => zipCode.City).
                            ThenInclude(zipCode => zipCode.Sector).
                            ThenInclude(sector => sector.Users).
                            FirstOrDefaultAsync(zipCode => zipCode.ID == id);

                if (zipCode is null)
                {
                    return NotFound("ZipCode not Found");
                }

                if (!Utils.UserHaveAccess(zipCode?.City?.Sector?.Users, accessUserId, _context))
                {
                    return Unauthorized("Invalid access token.");
                }

                return Ok(zipCode.ToZipCodeResponseDto());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateZipCode(CreateZipCodeRequest zipCodeRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //check if all ready exist
                var usingNameAndCodeZipCode = await _context.ZipCodes.FirstOrDefaultAsync(
                    zipCode => zipCode.Name.ToLower() == zipCodeRequest.Name.ToLower()
                    ||
                    zipCode.Code == zipCodeRequest.Code
                    );


                if (usingNameAndCodeZipCode is not null)
                {
                    return Conflict("Zip Code already exist.");
                }

                //check sector if exist 
                if (zipCodeRequest.CityID != null)
                {
                    var sector = await _context.Cities.FindAsync(zipCodeRequest.CityID);
                    if (sector is null)
                    {
                        return NotFound("City not exist.");
                    }
                }


                await _context.ZipCodes.AddAsync(zipCodeRequest.FromCreateZipCodeRequestDto());

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
        public async Task<IActionResult> UpdateZipCode(UpdateZipCodeRequest zipCodeRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //check if all ready exist
                var usingNameAndCodeZipCode = await _context.ZipCodes.FirstOrDefaultAsync(
                    zipCode => zipCode.Name.ToLower() == zipCodeRequest.Name.ToLower()
                    ||
                    zipCode.Code == zipCodeRequest.Code
                    );

                if (usingNameAndCodeZipCode is not null && usingNameAndCodeZipCode.ID != zipCodeRequest.ID)
                {
                    return Conflict("ZipCode info already exist.");
                }

                //get city from db 
                var dbZipCode = await _context.ZipCodes.FindAsync(zipCodeRequest.ID);

                if (dbZipCode is null)
                {
                    return NotFound("Zip Code Not found");
                }

                //check sector if exist 
                var city = await _context.Cities.FindAsync(zipCodeRequest.CityID);
                if (city is null)
                {
                    return NotFound("City not exist.");
                }

                dbZipCode.Name = zipCodeRequest.Name;
                dbZipCode.CityID = zipCodeRequest.CityID;
                dbZipCode.Code = zipCodeRequest.Code;

                await _context.SaveChangesAsync();

                return Ok("Zip Code updated.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteZipCode([FromHeader] String id)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }


                //get city from db 
                var dbZipCode = await _context.ZipCodes.FindAsync(id);

                if (dbZipCode is null)
                {
                    return NotFound("Zip code Not found");
                }
                _context.ZipCodes.Remove(dbZipCode);
                await _context.SaveChangesAsync();

                return Ok("Zip code Deleted.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }

    }


}