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
    [Route("sample")]
    [ApiController]
    public class SampleController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly String _SECRETKEY;

        public SampleController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _SECRETKEY = configuration["MySecretKey"];
        }


        [HttpGet]
        [Route("all-for-labs")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetSamplesForLabs()
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);


                //get samples from db
                var samples = await _context.Samples.
                            Include(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(samples => samples.Analyses).
                            Include(sample => sample.Lab).
                            OrderBy(samples => samples.SamplingDate).
                            ToArrayAsync();

                var accessibleSample = samples.Where(sample => sample.LabID == accessUserId).Select(sample => sample.ToSampleResponseDto());


                return Ok(accessibleSample);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("id-for-labs")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetSampleByIDFroLab([FromHeader] string id)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);


                //get cities from db
                var sample = await _context.Samples.
                            Include(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(sample => sample.Lab).
                            Include(samples => samples.Analyses).
                            FirstOrDefaultAsync(sample => sample.ID == id);

                if (sample is null)
                {
                    return NotFound("sample not Found");
                }

                if (!(sample.LabID == accessUserId))
                {
                    return Unauthorized("Invalid access token.");
                }

                return Ok(sample.ToSampleResponseDto());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetSamples()
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);


                //get samples from db
                var samples = await _context.Samples.
                            Include(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(samples => samples.Analyses).
                            Include(sample => sample.Lab).
                            OrderBy(samples => samples.SamplingDate).
                            ToArrayAsync();

                var accessibleSample = samples.Where(sample => Utils.UserHaveAccess(
                    sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users,
                    accessUserId,
                    _context
                )).Select(sample => sample.ToSampleResponseDto());


                return Ok(accessibleSample);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }

        [HttpGet]
        [Route("plot")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetSamplesByPlot([FromHeader] string plotID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);


                //get samples from db
                var samples = await _context.Samples.
                            Include(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(samples => samples.Analyses).
                            Include(sample => sample.Lab).
                            OrderBy(samples => samples.SamplingDate).
                            ToArrayAsync();

                var accessibleSample = samples.Where(sample => Utils.UserHaveAccess(
                    sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users,
                    accessUserId,
                    _context
                ) && sample.PlotID == plotID).Select(sample => sample.ToSampleResponseDto());


                return Ok(accessibleSample);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("exploitation")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetSamplesByExploitation([FromHeader] string exploitationID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);


                //get samples from db
                var samples = await _context.Samples.
                            Include(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(samples => samples.Analyses).
                            Include(sample => sample.Lab).
                            OrderBy(samples => samples.SamplingDate).
                            ToArrayAsync();

                var accessibleSample = samples.Where(sample => Utils.UserHaveAccess(
                    sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users,
                    accessUserId,
                    _context
                ) && sample.Plot.ExploitationID == exploitationID).Select(sample => sample.ToSampleResponseDto());


                return Ok(accessibleSample);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("land")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetSamplesByLand([FromHeader] string landID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);


                //get samples from db
                var samples = await _context.Samples.
                            Include(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(samples => samples.Analyses).
                            Include(sample => sample.Lab).
                            OrderBy(samples => samples.SamplingDate).
                            ToArrayAsync();

                var accessibleSample = samples.Where(sample => Utils.UserHaveAccess(
                    sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users,
                    accessUserId,
                    _context
                ) && sample.Plot.Exploitation.LandID == landID).Select(sample => sample.ToSampleResponseDto());


                return Ok(accessibleSample);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("farmer")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetSamplesByFarmer([FromHeader] string farmerID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);


                //get samples from db
                var samples = await _context.Samples.
                            Include(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(samples => samples.Analyses).
                            Include(sample => sample.Lab).
                            OrderBy(samples => samples.SamplingDate).
                            ToArrayAsync();

                var accessibleSample = samples.Where(sample => Utils.UserHaveAccess(
                    sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users,
                    accessUserId,
                    _context
                ) && sample.Plot.Exploitation.Land.FarmerID == farmerID).Select(sample => sample.ToSampleResponseDto());


                return Ok(accessibleSample);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("zipCode")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetSamplesByZipCode([FromHeader] string zipCodeID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);


                //get samples from db
                var samples = await _context.Samples.
                            Include(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(samples => samples.Analyses).
                            Include(sample => sample.Lab).
                            OrderBy(samples => samples.SamplingDate).
                            ToArrayAsync();

                var accessibleSample = samples.Where(sample => Utils.UserHaveAccess(
                    sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users,
                    accessUserId,
                    _context
                ) && sample.Plot.Exploitation.Land.Farmer.ZipCodeID == zipCodeID).Select(sample => sample.ToSampleResponseDto());


                return Ok(accessibleSample);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("city")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetSamplesByCity([FromHeader] int cityID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);


                //get samples from db
                var samples = await _context.Samples.
                            Include(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(samples => samples.Analyses).
                            OrderBy(samples => samples.SamplingDate).
                            Include(sample => sample.Lab).
                            ToArrayAsync();

                var accessibleSample = samples.Where(sample => Utils.UserHaveAccess(
                    sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users,
                    accessUserId,
                    _context
                ) && sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.CityID == cityID).Select(sample => sample.ToSampleResponseDto());


                return Ok(accessibleSample);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("sector")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetSamplesBySector([FromHeader] string sectorID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);


                //get samples from db
                var samples = await _context.Samples.
                            Include(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(samples => samples.Analyses).
                            Include(sample => sample.Lab).
                            OrderBy(samples => samples.SamplingDate).
                            ToArrayAsync();

                var accessibleSample = samples.Where(sample => Utils.UserHaveAccess(
                    sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users,
                    accessUserId,
                    _context
                ) && sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.SectorID == sectorID).Select(sample => sample.ToSampleResponseDto());


                return Ok(accessibleSample);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("id")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetSampleByID([FromHeader] string id)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);


                //get cities from db
                var sample = await _context.Samples.
                            Include(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(samples => samples.Analyses).
                            Include(sample => sample.Lab).
                            FirstOrDefaultAsync(sample => sample.ID == id);

                if (sample is null)
                {
                    return NotFound("sample not Found");
                }

                if (!Utils.UserHaveAccess(sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context))
                {
                    return Unauthorized("Invalid access token.");
                }

                return Ok(sample.ToSampleResponseDto());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpPut]
        [Authorize(Roles = "Agronomist, Pedologist")]
        public async Task<IActionResult> UpdateSample(UpdateSampleRequest sampleRequest)
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


                //get Sample from db 
                var dbSample = await _context.Samples.
                            Include(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(samples => samples.Analyses).
                            Include(sample => sample.Lab).
                            FirstOrDefaultAsync(sample => sample.ID == sampleRequest.ID);

                if (dbSample is null)
                {
                    return NotFound("Sample Not found");
                }


                if (!Utils.UserHaveAccess(dbSample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context))
                {
                    return Unauthorized("Invalid access token.");
                }

                //check plot if exist 
                var plot = await _context.Plots.FindAsync(sampleRequest.PlotID);
                if (plot is null)
                {
                    return NotFound("Plot not exist.");
                }

                dbSample.Reference = sampleRequest.Reference;
                dbSample.SamplingDate = sampleRequest.SamplingDate;
                dbSample.PlotID = sampleRequest.PlotID;

                await _context.SaveChangesAsync();

                return Ok("Sample updated.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Agronomist, Pedologist")]
        public async Task<IActionResult> CreateSample(CreateSampleRequest sampleRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }


                //check plot if exist 
                if (sampleRequest.PlotID != null)
                {
                    var plot = await _context.Plots.FindAsync(sampleRequest.PlotID);
                    if (plot is null)
                    {
                        return NotFound("Plot not exist.");
                    }
                }


                await _context.Samples.AddAsync(sampleRequest.FromCreateSampleRequestDto());

                await _context.SaveChangesAsync();

                return Created();

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Pedologist")]
        public async Task<IActionResult> DeleteSample([FromHeader] string id)
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


                //get Sample from db 
                var dbSample = await _context.Samples.
                            Include(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            Include(sample => sample.Lab).
                            Include(samples => samples.Analyses).
                            FirstOrDefaultAsync(sample => sample.ID == id);

                if (dbSample is null)
                {
                    return NotFound("Sample Not found");
                }


                if (!Utils.UserHaveAccess(dbSample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context))
                {
                    return Unauthorized("Invalid access token.");
                }


                _context.Samples.Remove(dbSample);
                await _context.SaveChangesAsync();

                return Ok("Sample Deleted.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }



        [HttpPut]
        [Route("status")]
        [Authorize(Roles = "Lab")]
        public async Task<IActionResult> UpdateSampleStatusToReceived([FromHeader] string id)
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


                //get Sample from db 
                var dbSample = await _context.Samples.
                            FirstOrDefaultAsync(sample => sample.ID == id);

                if (dbSample is null)
                {
                    return NotFound("Sample Not found");
                }


                if (dbSample.LabID != accessUserId)
                {
                    return Unauthorized("Invalid access token.");
                }

                if (dbSample.Status != SampleStatus.registered)
                {
                    return Conflict("Sample already received");
                }


                dbSample.Status = SampleStatus.received;
                await _context.SaveChangesAsync();

                return Ok("Sample status Updated.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }

    }
}