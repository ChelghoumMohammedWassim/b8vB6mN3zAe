namespace b8vB6mN3zAe.Dtos.City
{
    public class UpdateCityRequest
    {
        public int ID { get; set; }
        public required String Name { get; set; }
        public String? SectorID { get; set; }
    }
}