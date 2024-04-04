using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Models
{
    public class Sector
    {
        public String ID{ get; set; } = Guid.NewGuid().ToString();
        public required String Name { get; set; }
        public String CreatedDate { get; set; } = DateTime.Now.ToString();

        public  String LabID { get; set; }= String.Empty;
        public  Lab? Lab { get; set; }

        public List<User> Users { get; set; }= new List<User>();
        public List<UserSector> UsersSectors { get; set; }= new List<UserSector>();

        public List<City> Cities { get; set; } = new List<City>();
    }
}