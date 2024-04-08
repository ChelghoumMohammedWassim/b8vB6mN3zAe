using System.ComponentModel.DataAnnotations;

namespace b8vB6mN3zAe.Models
{
    public class UpdateSectorRequest
    {
        public required String ID{ get; set; }
        [Required(ErrorMessage = "Name is required")]
        public required String Name { get; set; }
        public  String? LabID { get; set; }
    }
}