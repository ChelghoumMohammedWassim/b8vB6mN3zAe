using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class PositionMappers
    {
        public static Position FromCreatePositionRequestDto(this PositionCreateRequest positionRequest, String landId)
        {
            return new Position
            {
                latitude= positionRequest.latitude,
                longitude= positionRequest.longitude,
                LandID= landId
            };
        }

        public static PostionResponse? ToPositionResponseDto(this Position? position)
        {
            if (position is null)
            {
                return null;
            }
            return new PostionResponse
            {
                ID = position.ID,
                latitude = position.latitude,
                longitude = position.longitude,
            };
        }
    }
}