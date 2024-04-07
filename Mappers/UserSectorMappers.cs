using b8vB6mN3zAe.Dtos.UserDtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class UserSectorMappers
    {
        public static UserSector FromCreateUserSectorRequestDto(this CreateUserSectorRequest userSectorRequest)
        {
            return new UserSector
            {
                SectorID = userSectorRequest.SectorID,
                UserID = userSectorRequest.UserID
            };
        }

        public static UserSectorResponse? ToUserSectorResponseDto(this UserSector? userSectorRequest)
        {
            if(userSectorRequest == null)
            {
                return null;
            }
            return new UserSectorResponse
            {
                ID = userSectorRequest.ID,
                CreatedDate = userSectorRequest.CreatedDate,
                Sector = userSectorRequest.Sector.ToSectorJoinResponseDto()
            };
        }

    }
}