
namespace b8vB6mN3zAe.Models
{
    public class Farmer
    {
       public String ID { get; set; } = Guid.NewGuid().ToString();
       public required String FullName { get; set; }
       public required String Address { get; set; }
       public required String PhoneNumber { get; set; }
       public required String? Email { get; set; }
       public required String NCNA { get; set; }
       public String CreatedDate { get; set; } = DateTime.Now.ToString();

       public String? ZipCodeID { get; set; }
       public ZipCode? ZipCode { get; set; }

       public List<Land> Lands { get; set; } = new List<Land>();
    }
}