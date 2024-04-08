using System.ComponentModel.DataAnnotations;

namespace b8vB6mN3zAe.Dtos
{
    public class UpdateUserRequest
    {
        [Required(ErrorMessage = "UserName is required")]
        public required String UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public required String CurrentPassword { get; set; }

        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public required String? NewPassword { get; set; }

        [Required(ErrorMessage = "FirstName is required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public required string LastName { get; set; }

        public required int City { get; set; }

        public String Address { get; set; } = String.Empty;

        [Required(ErrorMessage = "PhoneNumber is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format")]
        public required String PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required String Email { get; set; }

    }
}