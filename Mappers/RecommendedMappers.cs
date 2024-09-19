using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class RecommendedMappers
    {
        public static Recommendation FromCreateRecommendationRequestDto(this RecommendationCreateRequest recommendationCreateRequest, String userID)
        {
            return new Recommendation
            {
                Goal = recommendationCreateRequest.Goal,
                AnalysisID = recommendationCreateRequest.AnalysisID,
                UserID = userID
            };
        }

        public static RecommendationResponse? ToRecommendationResponseDto(this Recommendation? recommendation)
        {
            if (recommendation is null)
            {
                return null;
            }
            return new RecommendationResponse
            {
                ID = recommendation.ID,
                Goal = recommendation.Goal,
                Date = recommendation.Date,
                AnalysisID = recommendation.AnalysisID,
                Analysis = recommendation.Analysis.ToAnalysisResponseDto(),
                RecommendedFertilizers = recommendation.RecommendedFertilizers.Select(item => item.ToRecommendedFertilizerResponseDto()).ToList(),
            };
        }

    }
}