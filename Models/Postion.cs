namespace b8vB6mN3zAe.Models
{
    public class Position
    {
        public int ID { get; set; }
        public required decimal longitude { get; set; }
        public required decimal latitude { get; set; }
        public String CreatedDate { get; set; } = DateTime.Now.ToString();

        public required String LandID { get; set; }
        public Land? Land { get; set; }
    }
}