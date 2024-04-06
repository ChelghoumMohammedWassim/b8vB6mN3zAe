using b8vB6mN3zAe.Dtos.City;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class CityMappers
    {
        public static City FromCreateCityRequestDto(this CreateCityRequest cityRequest)
        {
            return new City
            {
                Name = cityRequest.Name,
                SectorID = cityRequest.SectorID
            };
        }

        public static CityResponse? ToCityResponseDto(this City? city)
        {
            if (city is null)
            {
                return null;
            }
            return new CityResponse
            {
                ID = city.ID,
                Name = city.Name,
                Sector = city.Sector.ToSectorJoinResponseDto(),
            };
        }

        public static CityJoinResponse? ToCityJoinResponseDto(this City? city)
        {
            if (city is null)
            {
                return null;
            }
            return new CityJoinResponse
            {
                ID = city.ID,
                Name = city.Name
            };
        }
    }
}