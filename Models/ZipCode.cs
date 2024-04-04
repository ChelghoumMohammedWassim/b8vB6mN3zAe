using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Models
{
    public class ZipCode
    {
        public String ID{ get; set; } = Guid.NewGuid().ToString();
        public required String Name { get; set; }
        public String CreatedDate { get; set; } = DateTime.Now.ToString();
        
        public required int CityID { get; set; }
        public required City City{ get; set; }

    }
}