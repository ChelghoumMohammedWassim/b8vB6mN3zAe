namespace b8vB6mN3zAe.Models
{
    public class UserSector
    {
        public String ID{ get; set; } = Guid.NewGuid().ToString();
        public String CreatedDate { get; set; } = DateTime.Now.ToString();
        public required String SectorID { get; set; }
        public required String UserID { get; set; }

        public required Sector Sector { get; set; }
        public required User User { get; set; }
    }
}