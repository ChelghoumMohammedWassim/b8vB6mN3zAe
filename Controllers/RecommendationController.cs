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
    [Route("recommendation")]
    [ApiController]
    public class RecommendationController : ControllerBase
    {

        private readonly ApplicationDBContext _context;
        private readonly String _SECRETKEY;

        public RecommendationController(ApplicationDBContext context, IConfiguration configuration)
        {
            _context = context;
            _SECRETKEY = configuration["MySecretKey"];
        }


        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetRecondition()
        {
            try
            {
                //get recommendations from db
                var recommendations = await _context.Recommendations
                                .Include(item => item.RecommendedFertilizers)
                                .Include(item => item.Analysis)
                                .ToArrayAsync();

                return Ok(recommendations.Select(recommendation => recommendation.ToRecommendationResponseDto()));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpGet]
        [Route("id")]
        [Authorize(Roles = "Agronomist, Pedologist,  Admin")]
        public async Task<IActionResult> GetReconditionBy([FromHeader] string id)
        {
            try
            {
                //get cities from db
                var recommendation = await _context.Recommendations
                            .Include(item => item.RecommendedFertilizers)
                            .Include(item => item.Analysis)
                            .FirstOrDefaultAsync(recommendation => recommendation.ID == id);


                if (recommendation is null)
                {
                    return NotFound("Recommendation not Found");
                }


                return Ok(recommendation.ToRecommendationResponseDto());
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server error.");
            }
        }


        [HttpPost]
        [Authorize(Roles = "Agronomist, Pedologist")]
        public async Task<IActionResult> CreateRecommendation(RecommendationCreateRequest recommendationRequest)
        {
            // Begin a transaction
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    //decode token to get user id
                    string accessToken = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("bearer ", "");
                    string accessUserId = Token.DecodeToken(accessToken, _SECRETKEY);

                    // Check request structure
                    if (!ModelState.IsValid)
                    {
                        return BadRequest("Invalid request structure.");
                    }

                    Analysis analysis = await _context.Analysis.Include(analyze => analyze.Sample).FirstOrDefaultAsync(r => r.ID == recommendationRequest.AnalysisID);
                    if (analysis is null || analysis.Status != AnalysisStatus.Accepted)
                    {
                        return NotFound("Analysis not found or not accepted.");
                    }

                    analysis.Sample.Status = SampleStatus.recommended;

                    // Create and add the recommendation
                    Recommendation dbRecommendation = recommendationRequest.FromCreateRecommendationRequestDto(accessUserId);
                    await _context.Recommendations.AddAsync(dbRecommendation);

                    // Process each recommended fertilizer
                    foreach (RecommendedFertilizerCreateRequest recommendedFertilizer in recommendationRequest.RecommendedFertilizers)
                    {
                        Fertilizer fertilizer = await _context.Fertilizers.FirstOrDefaultAsync(f => f.ID == recommendedFertilizer.FertilizerID);
                        if (fertilizer is null)
                        {
                            // If fertilizer is not found, rollback and return an error
                            await transaction.RollbackAsync();
                            return NotFound("Fertilizer not found.");
                        }

                        // Add recommended fertilizer
                        await _context.RecommendedFertilizers.AddAsync(
                            recommendedFertilizer.FromCreateRecommendedFertilizerRequestDto(dbRecommendation.ID));
                    }

                    // Commit the transaction if all operations succeed
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return Created();
                }
                catch (Exception)
                {
                    // Rollback the transaction in case of any exception
                    await transaction.RollbackAsync();
                    return StatusCode(500, "Internal server error.");
                }
            }
        }



        [HttpDelete]
        [Authorize(Roles = "Agronomist, Pedologist")]
        public async Task<IActionResult> DeleteRecommendation([FromHeader] string id)
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


                //get recommendation from db 
                var dbRecommendation = await _context.Recommendations.
                                            Include(r => r.RecommendedFertilizers)
                                            .FirstOrDefaultAsync(r => r.ID == id);

                if (dbRecommendation is null)
                {
                    return NotFound("Recommendation Not found.");
                }

                if (dbRecommendation.UserID != accessUserId)
                {
                    return Unauthorized("User can't delete this recommendation.");
                }

                foreach (RecommendedFertilizer recommendedFertilizer in dbRecommendation.RecommendedFertilizers)
                {
                    _context.RecommendedFertilizers.Remove(recommendedFertilizer);
                }

                _context.Recommendations.Remove(dbRecommendation);
                await _context.SaveChangesAsync();

                return Ok("Recommendation Deleted.");

            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error.");
            }

        }

    }
}