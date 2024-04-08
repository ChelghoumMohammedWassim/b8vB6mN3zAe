
namespace b8vB6mN3zAe.Dtos
{
    public class LabResponse
    {
        public required String UserName { get; set; }
        public required String Name { get; set; }
        public required CityJoinResponse? City { get; set; }
        public String Address { get; set; } = String.Empty;
        public required String PhoneNumber { get; set; }
        public required String Email { get; set; }
    }
}
