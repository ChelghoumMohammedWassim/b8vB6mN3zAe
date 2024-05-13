namespace b8vB6mN3zAe.Dtos
{
    public class LandResponse
    {
        public required String ID { get; set; }
        public required string Name { get; set; }
        public required int Rainfall { get; set; }

        public required FarmerJoinResponse Farmer { get; set; }

        public List<PostionResponse> Positions { get; set; } = new List<PostionResponse>();
    }
}