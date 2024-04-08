
using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class LabMappers
    {
        public static Lab FromCreateLabRequestDto(this CreateLabRequest labRequest)
        {
            return new Lab
            {
                UserName = labRequest.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(labRequest.Password),
                Name = labRequest.Name,
                CityID = labRequest.City,
                Address = labRequest.Address,
                PhoneNumber = labRequest.PhoneNumber,
                Email = labRequest.Email,
            };
        }

        public static LabResponse ToLabResponseDto(this Lab lab)
        {
            return new LabResponse
            {
                UserName = lab.UserName,
                Name = lab.Name,
                Email = lab.Email,
                Address = lab.Address,
                City = lab.City.ToCityJoinResponseDto(),
                PhoneNumber = lab.PhoneNumber,
            };
        }

        public static LabJoinResponse? ToLabJoinResponseDto(this Lab? lab)
        {
            if (lab is null)
            {
                return null;
            }
            return new LabJoinResponse
            {
                ID = lab.ID,
                Name = lab.Name,
                Email = lab.Email,
                Address = lab.Address,
                City = lab.City.ToCityJoinResponseDto(),
                PhoneNumber = lab.PhoneNumber,
            };
        }

        public static AdminLabListResponse ToAdminLabsListResponseDto(this Lab lab)
        {
            return new AdminLabListResponse
            {
                ID = lab.ID,
                UserName = lab.UserName,
                Name = lab.Name,
                City = lab.City.ToCityJoinResponseDto(),
                Address = lab.Address,
                PhoneNumber = lab.PhoneNumber,
                Email = lab.Email,
                IsActive = lab.IsActive,
                CreatedDate = lab.CreatedDate,
                Sectors = lab.Sectors.Select(sector=> sector.ToSectorJoinResponseDto()).ToList(),
            };
        }



    }
}