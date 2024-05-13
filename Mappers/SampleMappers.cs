using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class SampleMappers
    {
        public static Sample FromCreateSampleRequestDto(this CreateSampleRequest sampleRequest)
        {
            return new Sample
            {
                Reference= sampleRequest.Reference,
                SamplingDate= sampleRequest.SamplingDate.ToString(),
                PlotID= sampleRequest.PlotID,
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