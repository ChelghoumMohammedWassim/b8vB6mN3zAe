using b8vB6mN3zAe.Dtos.City;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class SectorMapper
    {
        public static Sector FromCreateSectorRequestDto(this CreateSectorRequest sectorRequest)
        {
            return new Sector
            {
                Name= sectorRequest.Name,
                LabID = sectorRequest.LabID,
            };
        }

        public static SectorResponse ToSectorResponseDto(this Sector? sector)
        {
            return new SectorResponse
            {
                ID = sector.ID,
                Name = sector.Name, 
                CreatedDate = sector.CreatedDate,
                Lab = sector.Lab.ToLabJoinResponseDto(),
                Cities = sector.Cities.Select(city=> city.ToCityJoinResponseDto()).ToList(),
            };
        }

        public static SectorJoinResponse ToSectorJoinResponseDto(this Sector sector)
        {
            if (sector is null)
            {
                return null;
            }
            return new SectorJoinResponse
            {
                ID = sector.ID,
                Name = sector.Name, 
            };
        }
    }
}