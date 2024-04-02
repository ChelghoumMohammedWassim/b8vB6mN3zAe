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

        public static UserResponse ToUserResponseDto(this User user)
        {
            return new UserResponse
            {
                UserName= user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address= user.Address,
                City = user.City,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role.ToString(),
            };
        }
        public static AdminUsersListResponse ToAdminUsersListResponseDto(this User user)
        {
            return new AdminUsersListResponse
            {
                ID = user.ID,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                City = user.City,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Email = user.Email,
                Role = user.Role,
                IsActive = user.IsActive,
                CreatedDate = user.CreatedDate
            };
        }

    }
}