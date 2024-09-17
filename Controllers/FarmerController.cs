using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Models;
using b8vB6mN3zAe.Models.Enums;
using b8vB6mN3zAe.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace b8vB6mN3zAe.Controllers
{
    [Route("farmer")]
    [ApiController]
    public class FarmerController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly String _SECRETKEY;

        public FarmerController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _SECRETKEY = configuration["MySecretKey"];
        }


        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetFarmer()
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

                //get farmers from db
                var farmers = await _context.Farmers.
                            Include(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(farmer => farmer.Lands).
                            ToArrayAsync();

                var accessibleFarmers = farmers.Where(farmer =>
                        Utils.UserHaveAccess(farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context)).
                        Select(farmer => farmer.ToFarmerResponseDto());

                return Ok(accessibleFarmers);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet]
        [Route("zipCode")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetFarmerByZipCode([FromHeader] string zipCodeID)
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

                //get farmers from db
                var farmers = await _context.Farmers.
                            Include(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(farmer => farmer.Lands).
                            ToArrayAsync();

                var accessibleFarmers = farmers.Where(farmer =>
                        Utils.UserHaveAccess(farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context) && farmer.ZipCodeID == zipCodeID).
                        Select(farmer => farmer.ToFarmerResponseDto());

                return Ok(accessibleFarmers);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet]
        [Route("city")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetFarmerByCity([FromHeader] int cityID)
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

                //get farmers from db
                var farmers = await _context.Farmers.
                            Include(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(farmer => farmer.Lands).
                            ToArrayAsync();

                var accessibleFarmers = farmers.Where(farmer =>
                        Utils.UserHaveAccess(farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context) && farmer.ZipCode.CityID == cityID).
                        Select(farmer => farmer.ToFarmerResponseDto());

                return Ok(accessibleFarmers);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }


        [HttpGet]
        [Route("sector")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetFarmerBySector([FromHeader] string sectorID)
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

                //get farmers from db
                var farmers = await _context.Farmers.
                            Include(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(farmer => farmer.Lands).
                            ToArrayAsync();

                var accessibleFarmers = farmers.Where(farmer =>
                        Utils.UserHaveAccess(farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context) && farmer.ZipCode.City.SectorID == sectorID).
                        Select(farmer => farmer.ToFarmerResponseDto());

                return Ok(accessibleFarmers);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }



        [HttpGet]
        [Route("id")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetFarmerBy([FromHeader] String id)
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


                //get farmer from db
                var farmer = await _context.Farmers.
                            Include(farmer => farmer.Lands).
                            Include(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            FirstOrDefaultAsync(farmer => farmer.ID == id);
                ;

                if (farmer is null)
                {
                    return NotFound("Farmer not Found.");
                }

                if (!Utils.UserHaveAccess(farmer.ZipCode.City.Sector.Users, accessUserId, _context))
                {
                    return Unauthorized("Invalid access token.");
                }

                return Ok(farmer.ToFarmerResponseDto());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpPut]
        [Authorize(Roles = "Agronomist, Pedologist")]
        public async Task<IActionResult> UpdateFarmer(UpdateFarmerRequest farmerRequest)
        {
            try
            {

                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //check if already exist
                var usingNCNAFarmer = await _context.Farmers.FirstOrDefaultAsync(farmer => farmer.NCNA == farmerRequest.NCNA);
                if (usingNCNAFarmer is not null && usingNCNAFarmer.NCNA != usingNCNAFarmer.NCNA)
                {
                    return Conflict("Farmer already exist.");
                }

                //get farmer from db 
                var dbFarmer = await _context.Farmers.
                                Include(farmer => farmer.ZipCode).
                                ThenInclude(zipCode => zipCode.City).
                                ThenInclude(city => city.Sector).
                                ThenInclude(sector => sector.Users).
                                FirstOrDefaultAsync(farmer => farmer.ID == farmerRequest.ID);

                if (dbFarmer is null)
                {
                    return NotFound("Farmer Not found.");
                }

                if (!Utils.UserHaveAccess(dbFarmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context))
                {
                    return Unauthorized("Invalid access token.");
                }

                //check zipCode if exist 
                var zipCode = await _context.ZipCodes.FindAsync(farmerRequest.ZipCodeID);
                if (zipCode is null)
                {
                    return NotFound("ZipCode not exist.");
                }

                dbFarmer.FullName = farmerRequest.FirstName;
                dbFarmer.Address = farmerRequest.Address;
                dbFarmer.ZipCodeID = farmerRequest.ZipCodeID;
                dbFarmer.PhoneNumber = farmerRequest.PhoneNumber;
                dbFarmer.Email = farmerRequest.Email;
                dbFarmer.NCNA = farmerRequest.NCNA;

                await _context.SaveChangesAsync();

                return Ok("Farmer updated.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Agronomist, Pedologist")]
        public async Task<IActionResult> CreateFarmer(CreateFarmerRequest farmerRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                //check if all ready exist
                var usingNCNAFarmer = await _context.Farmers.FirstOrDefaultAsync(farmer => farmer.NCNA == farmerRequest.NCNA);
                if (usingNCNAFarmer is not null)
                {
                    return Conflict("Farmer already exist.");
                }

                //check zip code
                var zipCode = await _context.ZipCodes.FindAsync(farmerRequest.ZipCodeID);
                if (zipCode is null)
                {
                    return NotFound("Zip Code not exist.");
                }


                await _context.Farmers.AddAsync(farmerRequest.FromCreateFarmerRequestDto());

                await _context.SaveChangesAsync();

                return Created();

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Agronomist")]
        public async Task<IActionResult> DeleteFarmer([FromHeader] String id)
        {
            try
            {

                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }


                //get farmer from db 
                var dbFarmer = await _context.Farmers.
                                Include(farmer => farmer.ZipCode).
                                ThenInclude(zipCode => zipCode.City).
                                ThenInclude(city => city.Sector).
                                ThenInclude(sector => sector.Users).
                                FirstOrDefaultAsync(farmer => farmer.ID == id);

                if (dbFarmer is null)
                {
                    return NotFound("Farmer Not found.");
                }

                if (!Utils.UserHaveAccess(dbFarmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context))
                {
                    return Unauthorized("Invalid access token.");
                }

                _context.Farmers.Remove(dbFarmer);
                await _context.SaveChangesAsync();

                return Ok("City Deleted.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }

    }
}