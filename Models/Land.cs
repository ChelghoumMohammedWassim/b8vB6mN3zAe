namespace b8vB6mN3zAe.Models
{
    public class Land
    {
        public string ID { get; set; } =  Guid.NewGuid().ToString();
        public required string Name { get; set; }
        public required int Rainfall { get; set; }
        public String CreatedDate { get; set; } = DateTime.Now.ToString();

        public required String FarmerID { get; set; }
        public  Farmer? Farmer { get; set; }
        public List<Exploitation> Exploitations { get; set; } = new List<Exploitation>();
    }
}