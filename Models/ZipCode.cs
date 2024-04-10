using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Models
{
    public class ZipCode
    {
        public String ID{ get; set; } = Guid.NewGuid().ToString();
        public required String Name { get; set; }
        public required int Code { get; set;}
        public String CreatedDate { get; set; } = DateTime.Now.ToString();
        
        public required int? CityID { get; set; }
        public  City? City{ get; set; }

        public List<Farmer> Farmers { get; set; }= new List<Farmer>();

    }
}