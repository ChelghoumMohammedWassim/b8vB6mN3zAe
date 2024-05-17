using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Dtos
{
    public class LandListResponse
    {
        public required String ID { get; set; }
        public required string Name { get; set; }
        public required int Rainfall { get; set; }

        public required FarmerJoinResponse Farmer { get; set; }
        public List<ExploitationListResponse> Exploitations { get; set; } = new List<ExploitationListResponse>();

    }
}