using b8vB6mN3zAe.Dtos;

namespace b8vB6mN3zAe.Model
{
    public class SectorResponse
    {
        public required String ID{ get; set; }
        public required String Name { get; set; }
        public required String CreatedDate { get; set; }
        public  LabJoinResponse? Lab { get; set; }
        public List<CityJoinResponse?> Cities { get; set; } = new List<CityJoinResponse?>();
    }
}