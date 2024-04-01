using b8vB6mN3zAe.Dtos.UserDtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class UserMappers
    {
        public static User FromCreateUserRequestDto(this CreateUserRequest userRequest)
        {
            return new User
            {
                UserName = userRequest.UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(userRequest.Password),
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                City = userRequest.City,
                Address = userRequest.Address,
                PhoneNumber = userRequest.PhoneNumber,
                Email = userRequest.Email,
                Role = userRequest.Role,
            };
        }



    }
}