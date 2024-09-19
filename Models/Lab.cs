using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Models
{
    public class Lab
    {
        public String ID{ get; set; } = Guid.NewGuid().ToString();
        public required String UserName { get; set; }
        public required String Password { get; set; }
        public required String Name { get; set; }
        public required int CityID { get; set; }
        public City? City { get; set; }
        public String Address { get; set; } = String.Empty;
        public required String PhoneNumber { get; set; }
        public required String Email { get; set; }
        public bool IsActive { get; set; } = true;
        public String CreatedDate { get; set; } = DateTime.Now.ToString();
        public List<Sector> Sectors { get; set; }= new List<Sector>();
        public List<Sample> Samples { get; set; } = new List<Sample>();
    }
}