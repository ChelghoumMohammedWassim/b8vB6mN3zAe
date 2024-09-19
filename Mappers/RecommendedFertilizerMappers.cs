using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class RecommendedFertilizerMappers
    {
        public static RecommendedFertilizer FromCreateRecommendedFertilizerRequestDto(this RecommendedFertilizerCreateRequest recommendedFertilizerRequest, string RecommendationID)
        {
            return new RecommendedFertilizer
            {
                Quantity = recommendedFertilizerRequest.Quantity,
                RecommendationID = RecommendationID,
                FertilizerID = recommendedFertilizerRequest.FertilizerID,
            };
        }

        public static RecommendedFertilizerResponse? ToRecommendedFertilizerResponseDto(this RecommendedFertilizer? recommendedFertilizer)
        {
            if (recommendedFertilizer is null)
            {
                return null;
            }
            return new RecommendedFertilizerResponse
            {
                Fertilizer = recommendedFertilizer?.Fertilizer?.ToFertilizerResponseDto(),
                Quantity = recommendedFertilizer!.Quantity
            };
        }

    }
}