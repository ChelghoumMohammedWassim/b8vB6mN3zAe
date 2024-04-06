using b8vB6mN3zAe.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace b8vB6mN3zAe.Models
{
    public class CreateSectorRequest
    {
        [Required(ErrorMessage = "Name is required")]
        public required String Name { get; set; }
        public required String? LabID { get; set; }
    }
}