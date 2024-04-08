using System.ComponentModel.DataAnnotations;
namespace b8vB6mN3zAe.Dtos
{
    public class CreateLabRequest
    {
        [Required(ErrorMessage = "UserName is required")]
        public required String UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public required String Password { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public required String Name { get; set; }

        [Required(ErrorMessage = "City is required")]
        public required int City { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public String Address { get; set; } = String.Empty;

        [Required(ErrorMessage = "PhoneNumber is required")]
        public required String PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public required String Email { get; set; }
    }
}