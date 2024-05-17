using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Models;
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

        public FarmerController(ApplicationDBContext context)
        {
            _context = context;
        }


        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Agronomist, Pedologist, Admin")]
        public async Task<IActionResult> GetFarmer()
        {
            try
            {
                //get farmers from db
                var farmers = await _context.Farmers.
                            Include(farmer => farmer.ZipCode).
                            Include(farmer=> farmer.Lands).
                            Select(farmer => farmer.ToFarmerResponseDto()).
                            ToArrayAsync();

                return Ok(farmers);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }

        [HttpGet]
        [Route("zipCode")]
        [Authorize(Roles = "Agronomist, Pedologist, Admin")]
        public async Task<IActionResult> GetFarmerByZipCode([FromHeader] String zipCodeID)
        {
            try
            {
                //get farmers from db
                var farmers = await _context.Farmers.
                            Where(farmer => farmer.ZipCodeID == zipCodeID).
                            Include(farmer => farmer.ZipCode).
                            Include(farmer=> farmer.Lands).
                            Select(farmer => farmer.ToFarmerResponseDto()).
                            ToArrayAsync();

                return Ok(farmers);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }

        [HttpGet]
        [Route("city")]
        [Authorize(Roles = "Agronomist, Pedologist, Admin")]
        public async Task<IActionResult> GetFarmerByCity([FromHeader] int cityID)
        {
            try
            {
                //get farmers from db
                var farmers = await _context.Farmers.
                            Include(farmer => farmer.ZipCode).
                            Where(farmer => farmer.ZipCode.CityID == cityID).
                            Include(farmer=> farmer.Lands).
                            Select(farmer => farmer.ToFarmerResponseDto()).
                            ToArrayAsync();

                return Ok(farmers);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }

        [HttpGet]
        [Route("sector")]
        [Authorize(Roles = "Agronomist, Pedologist, Admin")]
        public async Task<IActionResult> GetFarmerBySector([FromHeader] String sectoeID)
        {
            try
            {
                //get farmers from db
                var farmers = await _context.Farmers.
                            Include(farmer=> farmer.Lands).
                            Include(farmer => farmer.ZipCode).
                            ThenInclude(ZipCode => ZipCode.City).
                            Where(farmer => farmer.ZipCode.City.SectorID == sectoeID).
                            Select(farmer => farmer.ToFarmerResponseDto()).
                            ToArrayAsync();

                return Ok(farmers);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }

        [HttpGet]
        [Route("id")]
        [Authorize(Roles = "Agronomist, Pedologist, Admin")]
        public async Task<IActionResult> GetFarmerBy([FromHeader] String id)
        {
            try
            {
                //get farmer from db
                var farmer = await _context.Farmers.
                            Include(farmer=> farmer.Lands).
                            Include(farmer => farmer.ZipCode).
                            FirstOrDefaultAsync(farmer => farmer.ID == id);
                ;

                if (farmer is null)
                {
                    return NotFound("Farmer not Found");
                }

                return Ok(farmer.ToFarmerResponseDto());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpPut]
        [Authorize(Roles = "Agronomist, Pedologist, Admin")]
        public async Task<IActionResult> UpdateFarmer(UpdateFarmerRequest farmerRequest)
        {
            try
            {
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
                var dbFarmer = await _context.Farmers.FindAsync(farmerRequest.ID);

                if (dbFarmer is null)
                {
                    return NotFound("Farmer Not found");
                }

                //check zipCode if exist 
                var zipCode = await _context.ZipCodes.FindAsync(farmerRequest.ZipCodeID);
                if (zipCode is null)
                {
                    return NotFound("ZipCode not exist.");
                }

                dbFarmer.FirstName = farmerRequest.FirstName;
                dbFarmer.LastName = farmerRequest.LastName;
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
        [Authorize(Roles = "Agronomist, Pedologist, Admin")]
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
        [Authorize(Roles = "Agronomist, Pedologist, Admin")]
        public async Task<IActionResult> DeleteFarmer([FromHeader] String id)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }


                //get Farmer from db 
                var dbFarmer = await _context.Farmers.FindAsync(id);

                if (dbFarmer is null)
                {
                    return NotFound("Farmer Not found");
                }

                //check if he have history

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