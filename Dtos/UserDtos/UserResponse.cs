using System.ComponentModel.DataAnnotations;
using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Dtos.UserDtos
{
    public class UserResponse
    {
        public required String UserName { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required CityEnum City { get; set; }
        public String Address { get; set; } = String.Empty;
        public required String PhoneNumber { get; set; }
        public required String Email { get; set; }
        public required String Role { get; set; }
    }
}