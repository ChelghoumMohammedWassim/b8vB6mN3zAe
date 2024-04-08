using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class ZipCodeMappers
    {
        public static ZipCode FromCreateZipCodeRequestDto(this CreateZipCodeRequest zipCodeRequest)
        {
            return new ZipCode
            {
                Name = zipCodeRequest.Name,
                Code = zipCodeRequest.Code,
                CityID = zipCodeRequest.CityID,
            };
        }

        public static ZipCodeResponse? ToZipCodeResponseDto(this ZipCode? zipCode)
        {
            if (zipCode is null)
            {
                return null;
            }
            return new ZipCodeResponse
            {
                ID = zipCode.ID,
                Name = zipCode.Name,
                Code = zipCode.Code,
                City = zipCode.City.ToCityJoinResponseDto(),
            };
        }

        public static ZipCodeJoinResponse? ToZipCodeJoinResponseDto(this ZipCode? zipCode)
        {
            if (zipCode is null)
            {
                return null;
            }
            return new ZipCodeJoinResponse
            {
                ID = zipCode.ID,
                Name = zipCode.Name,
                Code = zipCode.Code,
            };
        }
    }
}