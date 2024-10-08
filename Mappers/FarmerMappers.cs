using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class FarmerMappers
    {
        public static Farmer FromCreateFarmerRequestDto(this CreateFarmerRequest farmerRequest)
        {
            return new Farmer
            {
                FullName = farmerRequest.FullName,
                
                Address = farmerRequest.Address,
                PhoneNumber = farmerRequest.PhoneNumber,
                Email = farmerRequest.Email,
                NCNA = farmerRequest.NCNA,
                ZipCodeID = farmerRequest.ZipCodeID,
            };
        }

        public static FarmerResponse? ToFarmerResponseDto(this Farmer? farmer)
        {
            if (farmer is null)
            {
                return null;
            }
            return new FarmerResponse
            {
                ID = farmer.ID,
                FullName = farmer.FullName,
                PhoneNumber = farmer.PhoneNumber,
                Email = farmer.Email,
                NCNA = farmer.NCNA,
                Address = farmer.Address,
                ZipCode = farmer.ZipCode.ToZipCodeJoinResponseDto(), 
                Lands = farmer.Lands.Select(land=> land.ToLandJoinResponseDto()).ToList(),
            };
        }

        public static FarmerJoinResponse? ToFarmerJoinResponseDto(this Farmer? farmer)
        {
            if (farmer is null)
            {
                return null;
            }
            return new FarmerJoinResponse
            {
                ID = farmer.ID,
                FullName = farmer.FullName,
                NCNA = farmer.NCNA
            };
        }
    }
}