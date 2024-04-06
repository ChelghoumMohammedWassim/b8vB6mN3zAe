using b8vB6mN3zAe.Dtos.City;
using b8vB6mN3zAe.Dtos.UserDtos;

namespace b8vB6mN3zAe.Models
{
    public class SectorResponse
    {
        public String ID{ get; set; }
        public required String Name { get; set; }
        public required String CreatedDate { get; set; }
        public  LabJoinResponse? Lab { get; set; }
        public List<CityJoinResponse?> Cities { get; set; } = new List<CityJoinResponse?>();
    }
}