using System.ComponentModel.DataAnnotations;
using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Dtos.UserDtos
{
    public class AdminUpdateUserRequest
    {
        public String ID { get; set; }= String.Empty;
        
        [Required(ErrorMessage = "FirstName is required")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public required string LastName { get; set; }

        public required City City { get; set; }

        public String Address { get; set; } = String.Empty;

        [Required(ErrorMessage = "PhoneNumber is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format")]
        public required String PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public required String Email { get; set; }

        public required bool IsActive { get; set; }

        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public String? Password { get; set; }= null;

    }
}