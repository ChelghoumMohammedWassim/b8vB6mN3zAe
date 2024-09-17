using b8vB6mN3zAe.Database;
using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Mappers;
using b8vB6mN3zAe.Models;
using b8vB6mN3zAe.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace b8vB6mN3zAe.Controllers
{
    [Route("plot")]
    [ApiController]
    public class PlotController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly String _SECRETKEY;

        public PlotController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _SECRETKEY = configuration["MySecretKey"];
        }


        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetPlots()
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                var plots = await _context.Plots.
                        Include(plot => plot.Positions).
                        Include(plot => plot.Exploitation).
                        ThenInclude(exploitation => exploitation.Land).
                        ThenInclude(land => land.Farmer).
                        ThenInclude(farmer => farmer.ZipCode).
                        ThenInclude(zipCode => zipCode.City).
                        ThenInclude(city => city.Sector).
                        ThenInclude(sector => sector.Users).
                        Include(plot => plot.Samples).
                        ThenInclude(sample => sample.Analyses)
                        .ToListAsync();

                var accessiblePlots = plots.Where(plot => Utils.UserHaveAccess(
                                plot.Exploitation.Land.Farmer.ZipCode.City.Sector.Users,
                                accessUserId,
                                _context
                            )).Select(plot => plot.ToPlotResponseDto());


                return Ok(accessiblePlots);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("exploitation")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetPlotsByExploitation([FromHeader] string exploitationID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                var plots = await _context.Plots.
                        Include(plot => plot.Positions).
                        Include(plot => plot.Exploitation).
                        ThenInclude(exploitation => exploitation.Land).
                        ThenInclude(land => land.Farmer).
                        ThenInclude(farmer => farmer.ZipCode).
                        ThenInclude(zipCode => zipCode.City).
                        ThenInclude(city => city.Sector).
                        ThenInclude(sector => sector.Users).
                        Include(plot => plot.Samples).
                        ThenInclude(sample => sample.Analyses)
                        .ToListAsync();

                var accessiblePlots = plots.Where(plot => Utils.UserHaveAccess(
                                plot.Exploitation.Land.Farmer.ZipCode.City.Sector.Users,
                                accessUserId,
                                _context
                            )&& plot.ExploitationID ==exploitationID).Select(plot => plot.ToPlotResponseDto());


                return Ok(accessiblePlots);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }



        [HttpGet]
        [Route("land")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetPlotsByLand([FromHeader] string landID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                var plots = await _context.Plots.
                        Include(plot => plot.Positions).
                        Include(plot => plot.Exploitation).
                        ThenInclude(exploitation => exploitation.Land).
                        ThenInclude(land => land.Farmer).
                        ThenInclude(farmer => farmer.ZipCode).
                        ThenInclude(zipCode => zipCode.City).
                        ThenInclude(city => city.Sector).
                        ThenInclude(sector => sector.Users).
                        Include(plot => plot.Samples).
                        ThenInclude(sample => sample.Analyses)
                        .ToListAsync();

                var accessiblePlots = plots.Where(plot => Utils.UserHaveAccess(
                                plot.Exploitation.Land.Farmer.ZipCode.City.Sector.Users,
                                accessUserId,
                                _context
                            )&& plot.Exploitation.LandID == landID).Select(plot => plot.ToPlotResponseDto());


                return Ok(accessiblePlots);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }



        [HttpGet]
        [Route("farmer")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetPlotsByFarmer([FromHeader] string farmerID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                var plots = await _context.Plots.
                        Include(plot => plot.Positions).
                        Include(plot => plot.Exploitation).
                        ThenInclude(exploitation => exploitation.Land).
                        ThenInclude(land => land.Farmer).
                        ThenInclude(farmer => farmer.ZipCode).
                        ThenInclude(zipCode => zipCode.City).
                        ThenInclude(city => city.Sector).
                        ThenInclude(sector => sector.Users).
                        Include(plot => plot.Samples).
                        ThenInclude(sample => sample.Analyses)
                        .ToListAsync();

                var accessiblePlots = plots.Where(plot => Utils.UserHaveAccess(
                                plot.Exploitation.Land.Farmer.ZipCode.City.Sector.Users,
                                accessUserId,
                                _context
                            )&& plot.Exploitation.Land.FarmerID == farmerID).Select(plot => plot.ToPlotResponseDto());


                return Ok(accessiblePlots);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("zipCode")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetPlotsByZipCode([FromHeader] string zipCodeID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                var plots = await _context.Plots.
                        Include(plot => plot.Positions).
                        Include(plot => plot.Exploitation).
                        ThenInclude(exploitation => exploitation.Land).
                        ThenInclude(land => land.Farmer).
                        ThenInclude(farmer => farmer.ZipCode).
                        ThenInclude(zipCode => zipCode.City).
                        ThenInclude(city => city.Sector).
                        ThenInclude(sector => sector.Users).
                        Include(plot => plot.Samples).
                        ThenInclude(sample => sample.Analyses)
                        .ToListAsync();

                var accessiblePlots = plots.Where(plot => Utils.UserHaveAccess(
                                plot.Exploitation.Land.Farmer.ZipCode.City.Sector.Users,
                                accessUserId,
                                _context
                            )&& plot.Exploitation.Land.Farmer.ZipCodeID == zipCodeID).Select(plot => plot.ToPlotResponseDto());


                return Ok(accessiblePlots);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }



        [HttpGet]
        [Route("city")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetPlotsByCity([FromHeader] int cityID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                var plots = await _context.Plots.
                        Include(plot => plot.Positions).
                        Include(plot => plot.Exploitation).
                        ThenInclude(exploitation => exploitation.Land).
                        ThenInclude(land => land.Farmer).
                        ThenInclude(farmer => farmer.ZipCode).
                        ThenInclude(zipCode => zipCode.City).
                        ThenInclude(city => city.Sector).
                        ThenInclude(sector => sector.Users).
                        Include(plot => plot.Samples).
                        ThenInclude(sample => sample.Analyses)
                        .ToListAsync();

                var accessiblePlots = plots.Where(plot => Utils.UserHaveAccess(
                                plot.Exploitation.Land.Farmer.ZipCode.City.Sector.Users,
                                accessUserId,
                                _context
                            )&& plot.Exploitation.Land.Farmer.ZipCode.CityID == cityID).Select(plot => plot.ToPlotResponseDto());


                return Ok(accessiblePlots);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }

        [HttpGet]
        [Route("sector")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetPlots([FromHeader] string sectorID)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                var plots = await _context.Plots.
                        Include(plot => plot.Positions).
                        Include(plot => plot.Exploitation).
                        ThenInclude(exploitation => exploitation.Land).
                        ThenInclude(land => land.Farmer).
                        ThenInclude(farmer => farmer.ZipCode).
                        ThenInclude(zipCode => zipCode.City).
                        ThenInclude(city => city.Sector).
                        ThenInclude(sector => sector.Users).
                        Include(plot => plot.Samples).
                        ThenInclude(sample => sample.Analyses)
                        .ToListAsync();

                var accessiblePlots = plots.Where(plot => Utils.UserHaveAccess(
                                plot.Exploitation.Land.Farmer.ZipCode.City.Sector.Users,
                                accessUserId,
                                _context
                            )&& plot.Exploitation.Land.Farmer.ZipCode.City.SectorID == sectorID).Select(plot => plot.ToPlotResponseDto());


                return Ok(accessiblePlots);

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        


        [HttpGet]
        [Route("id")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetPlotByID([FromHeader] String id)
        {
            try
            {

                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                var plot = await _context.Plots.
                        Include(plot => plot.Positions).
                        Include(plot => plot.Exploitation).
                        ThenInclude(exploitation => exploitation.Land).
                        ThenInclude(land => land.Farmer).
                        ThenInclude(farmer => farmer.ZipCode).
                        ThenInclude(zipCode => zipCode.City).
                        ThenInclude(city => city.Sector).
                        ThenInclude(sector => sector.Users).
                        Include(plot => plot.Samples).
                        ThenInclude(sample => sample.Analyses).
                        FirstOrDefaultAsync(plot => plot!.ID == id);

                if (plot is null)
                {
                    return NotFound("plot Not found.");
                }

                if (!Utils.UserHaveAccess(plot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context))
                {
                    return Unauthorized("Invalid access token.");
                }

                return Ok(plot.ToPlotResponseDto());

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }



        [HttpPost]
        [Authorize(Roles = "Agronomist, Pedologist")]
        public async Task<IActionResult> CreatePlot(CreatePlotRequest plotRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }
                //check request farmer if exist 
                var dbExploitation = await _context.Exploitations.FindAsync(plotRequest.ExploitationID);
                if (dbExploitation is null)
                {
                    return NotFound("Selected Exploitation not found.");
                }

                // Check if a plot with the same name already exists
                var dbPlot = await _context.Plots
                    .FirstOrDefaultAsync(plot => plot.Name == plotRequest.Name);

                if (dbPlot is not null)
                {
                    var isSamePosition = dbPlot.Positions.Count == plotRequest.Positions.Count &&
                        dbPlot.Positions.All(pos =>
                            plotRequest.Positions.Any(reqPos =>
                                reqPos.longitude == pos.longitude && reqPos.latitude == pos.latitude));

                    if (isSamePosition)
                    {
                        return Conflict("A Plot with the same name and positions already exists.");
                    }
                }


                Plot newPlot = plotRequest.FromCreatePlotRequestDto();

                //add plot to db
                await _context.Plots.AddAsync(newPlot);

                //add potions
                foreach (PositionCreateRequest p in plotRequest.Positions)
                {
                    await _context.Positions.AddAsync(p.FromCreatePositionRequestDto(newPlot.ID));
                }

                await _context.SaveChangesAsync();

                return Created();

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPut]
        [Authorize(Roles = "Agronomist, Pedologist")]
        public async Task<IActionResult> UpdatePlot(UpdatePlotRequest plotRequest)
        {
            try
            {
                //decode token to get user id
                string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request structure.");
                }

                var dbPlot = await _context.Plots.Include(plot => plot.Positions).FirstOrDefaultAsync(land => land.ID == plotRequest.ID);
                if (dbPlot is null)
                {
                    return NotFound("Plot Not Found.");
                }

                //check request farmer if exist 
                var dbExploitation = await _context.Plots.
                        Include(plot => plot.Positions).
                        Include(plot => plot.Exploitation).
                        ThenInclude(exploitation => exploitation.Land).
                        ThenInclude(land => land.Farmer).
                        ThenInclude(farmer => farmer.ZipCode).
                        ThenInclude(zipCode => zipCode.City).
                        ThenInclude(city => city.Sector).
                        ThenInclude(sector => sector.Users).
                        Include(plot => plot.Samples).
                        ThenInclude(sample => sample.Analyses).
                        FirstOrDefaultAsync(plot => plot!.ID == plotRequest.ExploitationID);
                
                if (dbExploitation is null)
                {
                    return NotFound("Selected Exploitation not found.");
                }

                if (!Utils.UserHaveAccess(dbPlot?.Exploitation?.Land?.Farmer?.ZipCode?.City?.Sector?.Users, accessUserId, _context))
                {
                    return Unauthorized("Invalid access token.");
                }


                // Check if a land with the same name already exists
                var UsingNameOrPositionsPlot = await _context.Plots
                    .Include(plot => plot.Positions)
                    .FirstOrDefaultAsync(plot => plot.Name == plotRequest.Name && plot.ID != plotRequest.ID);

                if (UsingNameOrPositionsPlot is not null)
                {
                    var isSamePosition = UsingNameOrPositionsPlot.Positions.Count == plotRequest.Positions.Count &&
                        UsingNameOrPositionsPlot.Positions.All(pos =>
                            plotRequest.Positions.Any(reqPos =>
                                reqPos.longitude == pos.longitude && reqPos.latitude == pos.latitude));

                    if (isSamePosition)
                    {
                        return Conflict("A Plot with the same name and positions already exists.");
                    }
                }

                //delete old positions
                foreach (Position p in dbPlot.Positions)
                {
                    _context.Positions.Remove(p);
                }


                //update the land
                dbPlot.Name = plotRequest.Name;
                dbPlot.Polygon = plotRequest.Polygon;
                dbPlot.Surface = plotRequest.Surface;
                dbPlot.Production = plotRequest.Production;
                dbPlot.TreeAge = plotRequest.TreeAge;
                dbPlot.Width = plotRequest.Width;
                dbPlot.Length = plotRequest.Length;
                dbPlot.Type = plotRequest.Type;
                dbPlot.ExploitationID = plotRequest.ExploitationID;

                //add new potions
                foreach (PositionCreateRequest p in plotRequest.Positions)
                {
                    await _context.Positions.AddAsync(p.FromCreatePositionRequestDto(plotRequest.ID));
                }

                await _context.SaveChangesAsync();

                return Ok("Plot Updated.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

    }


}