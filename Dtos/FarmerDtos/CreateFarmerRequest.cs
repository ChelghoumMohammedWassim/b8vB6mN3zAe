using System.ComponentModel.DataAnnotations;

namespace b8vB6mN3zAe.Dtos
{
    public class CreateFarmerRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public required String FullName { get; set; }
    
        
        [Required(ErrorMessage = "Address is required")]
        public required String Address { get; set; }
        
        [Required(ErrorMessage = "PhoneNumber is required")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number format")]
        public required String PhoneNumber { get; set; }
        
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public String? Email { get; set; } = null;

        [Required(ErrorMessage = "NCNA is required")]
        [RegularExpression(@"^\d", ErrorMessage = "Invalid NCNA format")]
        public required String NCNA { get; set; }

        public required String ZipCodeID { get; set; }
    }
}