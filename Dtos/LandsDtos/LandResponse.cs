using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Dtos
{
    public class LandResponse
    {
        public required String ID { get; set; }
        public required string Name { get; set; }
        public required double Rainfall { get; set; }

        public required FarmerJoinResponse? Farmer { get; set; }

    }
}