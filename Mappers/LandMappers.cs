using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class LandMappers
    {
        public static Land FromCreateLandRequestDto(this LandCreateRequest landRequest)
        {
            return new Land
            {
                Name = landRequest.Name,
                Rainfall = landRequest.Rainfall,
                FarmerID = landRequest.FarmerID,
            };
        }

        public static LandResponse? ToLandResponseDto(this Land? land)
        {
            if (land is null)
            {
                return null;
            }
            return new LandResponse
            {
                ID = land.ID,
                Name = land.Name,
                Rainfall = land.Rainfall,
                Farmer = land.Farmer.ToFarmerJoinResponseDto(),
                Positions = land.Positions.Select(land=> land.ToPositionResponseDto()).ToList(),
            };
        }

        public static LandJoinResponse? ToLandJoinResponseDto(this Land? land)
        {
            if (land is null)
            {
                return null;
            }
            return new LandJoinResponse
            {
                ID = land.ID,
                Name = land.Name,
                Rainfall = land.Rainfall,
                Positions = land.Positions.Select(land=> land.ToPositionResponseDto()).ToList(),
            };
        }
    }
}