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
            };
        }

        public static LandListResponse? ToLandListResponseDto(this Land? land)
        {
            if (land is null)
            {
                return null;
            }
            return new LandListResponse
            {
                ID = land.ID,
                Name = land.Name,
                Rainfall = land.Rainfall,
                Farmer = land.Farmer.ToFarmerJoinResponseDto(),
                Exploitations = land.Exploitations.Select(e=> e.ToExploitationListResponseDto()).ToList()
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
            };
        }
    }
}