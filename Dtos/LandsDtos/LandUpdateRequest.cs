namespace b8vB6mN3zAe.Dtos
{
    public class LandUpdateRequest
    {
        public required String ID { get; set; }
        public required string Name { get; set; }
        public required double Rainfall { get; set; }

        public required String FarmerID { get; set; }
    }
}