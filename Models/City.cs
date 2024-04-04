
namespace b8vB6mN3zAe.Models
{
    public class City
    {
       public int ID { get; set; }
       public required String Name { get; set; }
       public String CreatedDate { get; set; } = DateTime.Now.ToString();

       public List<ZipCode> ZipCodes { get; set; }= new List<ZipCode>();

       public string? SectorID { get; set; } = null;
       public Sector? Sector { get; set; } = null;

       public List<User> Users { get; set; } = new List<User>();
       public List<Lab> Labs { get; set; } = new List<Lab>();

    }
}