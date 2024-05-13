using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class PlotMappers
    {
        public static Plot FromCreatePlotRequestDto(this CreatePlotRequest plotRequest)
        {
            return new Plot
            {
                Name = plotRequest.Name,
                Polygon = plotRequest.Polygon,
                Surface = plotRequest.Surface,
                Production = plotRequest.Production,
                TreeAge = plotRequest.TreeAge,
                Width = plotRequest.Width,
                Type = plotRequest.Type,
                ExploitationID = plotRequest.ExploitationID,
                Length = plotRequest.Length,
            };
        }

        // public static CityResponse? ToCityResponseDto(this City? city)
        // {
        //     if (city is null)
        //     {
        //         return null;
        //     }
        //     return new CityResponse
        //     {
        //         ID = city.ID,
        //         Name = city.Name,
        //         Sector = city.Sector.ToSectorJoinResponseDto(),
        //         ZipCodes = city.ZipCodes.Select(zipCode=> zipCode.ToZipCodeJoinResponseDto()).ToList()
        //     };
        // }

        // public static CityJoinResponse? ToCityJoinResponseDto(this City? city)
        // {
        //     if (city is null)
        //     {
        //         return null;
        //     }
        //     return new CityJoinResponse
        //     {
        //         ID = city.ID,
        //         Name = city.Name
        //     };
        // }
    }
}