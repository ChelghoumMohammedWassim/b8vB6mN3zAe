using System.ComponentModel.DataAnnotations;

namespace b8vB6mN3zAe.Dtos
{
    public class AdminUpdateLabRequest
    {
        public required String ID { get; set; }

        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public required String? Password { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public required string Name { get; set; }
        
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