namespace b8vB6mN3zAe.Models.Enums
{
    public class City
    {
       public int ID { get; set; }
       public required String Name { get; set; }
       public String CreatedDate { get; set; } = DateTime.Now.ToString();

       public List<User>? Users { get; set; } = new List<User>();

       public List<ZipCode> ZipCodes { get; set; }= new List<ZipCode>();

       public String? SectorID { get; set; } = String.Empty;
       public Sector? Sector { get; set; }

    }
}