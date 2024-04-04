using b8vB6mN3zAe.Dtos.City;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class CityMappers
    {
        public static City FromCreateLabRequestDto(this CreateCityRequest cityRequest)
        {
            return new City
            {
                Name = cityRequest.Name,
                SectorID = cityRequest.SectorID
            };
        }

        public static CityResponse ToLabResponseDto(this City city)
        {
            return new CityResponse
            {
                ID = city.ID,
                Name = city.Name,
            };
        }
    }
}