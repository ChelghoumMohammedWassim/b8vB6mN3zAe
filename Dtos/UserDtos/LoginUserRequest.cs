using System.ComponentModel.DataAnnotations;
using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Dtos.UserDtos
{
    public class LoginUserRequest
    {
        public required String UserName { get; set; }
        public required String Password { get; set; }
    }
}