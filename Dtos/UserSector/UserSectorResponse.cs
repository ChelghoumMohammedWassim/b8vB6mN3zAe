namespace b8vB6mN3zAe.Models
{
    public class UserSectorResponse
    {
        public required String ID{ get; set; }
        public required String CreatedDate { get; set; }
        public required SectorJoinResponse? Sector { get; set; }

    }
}