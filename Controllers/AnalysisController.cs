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
    [Route("analysis")]
    [ApiController]
    public class AnalysisController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly String _SECRETKEY;

        public AnalysisController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _SECRETKEY = configuration["MySecretKey"];
        }


        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetAnalysis()
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //get analysis from db
                var analysis = await _context.Analysis.
                            Include(analysis => analysis.Sample).
                            ThenInclude(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            ToArrayAsync();

                var accessibleAnalysis = analysis
                                        .Where(analyze => Utils.UserHaveAccess(analyze?.Sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context))
                                        .Select(analyze => analyze.ToAnalysisResponseDto());

                return Ok(accessibleAnalysis);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("sample")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetAnalysisBySample([FromHeader] string sampleID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //get analysis from db
                var analysis = await _context.Analysis.
                            Include(analysis => analysis.Sample).
                            ThenInclude(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            ToArrayAsync();

                var accessibleAnalysis = analysis
                                        .Where(analyze => Utils.UserHaveAccess(analyze?.Sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context)
                                        && analyze.SampleID == sampleID
                                        )
                                        .Select(analyze => analyze.ToAnalysisResponseDto());

                return Ok(accessibleAnalysis);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }



        [HttpGet]
        [Route("plot")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetAnalysisByPlot([FromHeader] string plotID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //get analysis from db
                var analysis = await _context.Analysis.
                            Include(analysis => analysis.Sample).
                            ThenInclude(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            ToArrayAsync();

                var accessibleAnalysis = analysis
                                        .Where(analyze => Utils.UserHaveAccess(analyze?.Sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context)
                                        && analyze.Sample.PlotID == plotID)
                                        .Select(analyze => analyze.ToAnalysisResponseDto());

                return Ok(accessibleAnalysis);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }



        [HttpGet]
        [Route("exploitation")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetAnalysisByExploitation([FromHeader] string exploitationID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //get analysis from db
                var analysis = await _context.Analysis.
                            Include(analysis => analysis.Sample).
                            ThenInclude(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            ToArrayAsync();

                var accessibleAnalysis = analysis
                                        .Where(analyze => Utils.UserHaveAccess(analyze?.Sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context)
                                        && analyze.Sample.Plot.ExploitationID == exploitationID)
                                        .Select(analyze => analyze.ToAnalysisResponseDto());

                return Ok(accessibleAnalysis);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }



        [HttpGet]
        [Route("land")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetAnalysisByLand([FromHeader] string landID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //get analysis from db
                var analysis = await _context.Analysis.
                            Include(analysis => analysis.Sample).
                            ThenInclude(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            ToArrayAsync();

                var accessibleAnalysis = analysis
                                        .Where(analyze => Utils.UserHaveAccess(analyze?.Sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context)
                                        && analyze.Sample.Plot.Exploitation.LandID == landID)
                                        .Select(analyze => analyze.ToAnalysisResponseDto());

                return Ok(accessibleAnalysis);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("farmer")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetAnalysisByFarmer([FromHeader] string farmerID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //get analysis from db
                var analysis = await _context.Analysis.
                            Include(analysis => analysis.Sample).
                            ThenInclude(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            ToArrayAsync();

                var accessibleAnalysis = analysis
                                        .Where(analyze => Utils.UserHaveAccess(analyze?.Sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context)
                                        && analyze.Sample.Plot.Exploitation.Land.FarmerID == farmerID)
                                        .Select(analyze => analyze.ToAnalysisResponseDto());

                return Ok(accessibleAnalysis);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }



        [HttpGet]
        [Route("zipCode")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetAnalysisByZipCode([FromHeader] string zipCodeID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //get analysis from db
                var analysis = await _context.Analysis.
                            Include(analysis => analysis.Sample).
                            ThenInclude(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            ToArrayAsync();

                var accessibleAnalysis = analysis
                                        .Where(analyze => Utils.UserHaveAccess(analyze?.Sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context)
                                        && analyze.Sample.Plot.Exploitation.Land.Farmer.ZipCodeID == zipCodeID)
                                        .Select(analyze => analyze.ToAnalysisResponseDto());

                return Ok(accessibleAnalysis);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("city")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetAnalysisByCity([FromHeader] int cityID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //get analysis from db
                var analysis = await _context.Analysis.
                            Include(analysis => analysis.Sample).
                            ThenInclude(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            ToArrayAsync();

                var accessibleAnalysis = analysis
                                        .Where(analyze => Utils.UserHaveAccess(analyze?.Sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context)
                                        && analyze?.Sample?.Plot?.Exploitation?.Land.Farmer?.ZipCode?.CityID == cityID)
                                        .Select(analyze => analyze.ToAnalysisResponseDto());

                return Ok(accessibleAnalysis);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("sector")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetAnalysisBySector([FromHeader] string sectorID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //get analysis from db
                var analysis = await _context.Analysis.
                            Include(analysis => analysis.Sample).
                            ThenInclude(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            ToArrayAsync();

                var accessibleAnalysis = analysis
                                        .Where(analyze => Utils.UserHaveAccess(analyze?.Sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context)
                                        && analyze?.Sample?.Plot?.Exploitation?.Land.Farmer?.ZipCode?.City?.SectorID == sectorID)
                                        .Select(analyze => analyze.ToAnalysisResponseDto());

                return Ok(accessibleAnalysis);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }




        [HttpGet]
        [Route("id")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetAnalyzeByID([FromHeader] string id)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                //get cities from db
                var analyze = await _context.Analysis.
                            Include(analysis => analysis.Sample).
                            ThenInclude(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            FirstOrDefaultAsync(analyze => analyze.ID == id);

                if (analyze is null)
                {
                    return NotFound("Analyze not Found.");
                }

                if (!Utils.UserHaveAccess(analyze?.Sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context))
                {
                    return Unauthorized("Invalid access token.");
                }

                return Ok(analyze);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Lab")]
        public async Task<IActionResult> CreateAnalyze(AnalysisCreateRequest analysisRequest)
        {
            try
            {
                //check request structure
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }


                //check plot if exist 
                if (analysisRequest.SampleID != null)
                {
                    var sample = await _context.Samples.FindAsync(analysisRequest.SampleID);
                    if (sample is null)
                    {
                        return NotFound("Sample not exist.");
                    }
                }


                await _context.Analysis.AddAsync(analysisRequest.FromAnalysisCreateRequest());

                await _context.SaveChangesAsync();

                return Created();

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAnalyze([FromHeader] string id)
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


                //get cities from db
                var dbAnalyze = await _context.Analysis.
                            Include(analysis => analysis.Sample).
                            ThenInclude(sample => sample.Plot).
                            ThenInclude(plot => plot.Exploitation).
                            ThenInclude(exploitation => exploitation.Land).
                            ThenInclude(land => land.Farmer).
                            ThenInclude(farmer => farmer.ZipCode).
                            ThenInclude(zipCode => zipCode.City).
                            ThenInclude(city => city.Sector).
                            ThenInclude(sector => sector.Users).
                            FirstOrDefaultAsync(analyze => analyze.ID == id);

                if (dbAnalyze is null)
                {
                    return NotFound("Analyze Not found");
                }


                if (!Utils.UserHaveAccess(dbAnalyze?.Sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context))
                {
                    return Unauthorized("Invalid access token.");
                }


                _context.Analysis.Remove(dbAnalyze);
                await _context.SaveChangesAsync();

                return Ok("Analyze Deleted.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }



        [HttpPut]
        [Route("status")]
        [Authorize(Roles = "Agronomist, Pedologist")]
        public async Task<IActionResult> UpdateAnalyzeStatus(AnalysisStatusUpdateRequest analysisStatusUpdateRequest)
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


                //get cities from db
                var dbAnalyze = await _context.Analysis.
                            FirstOrDefaultAsync(analyze => analyze.ID == analysisStatusUpdateRequest.AnalyzeID);

                if (dbAnalyze is null)
                {
                    return NotFound("Analyze Not found");
                }


                if (!Utils.UserHaveAccess(dbAnalyze?.Sample?.Plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context))
                {
                    return Unauthorized("Invalid access token.");
                }

                if (analysisStatusUpdateRequest.AnalysisStatus == AnalysisStatus.Pending)
                {
                    return Unauthorized("User can't set analyze pending.");
                }


                dbAnalyze.Status = analysisStatusUpdateRequest.AnalysisStatus;
                await _context.SaveChangesAsync();

                return Ok("Analyze status updated.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }

    }
}