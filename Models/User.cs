using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Models
{
    public class User
    {
        public String ID{ get; set; } = Guid.NewGuid().ToString();
        public required String UserName { get; set; }
        public required String Password { get; set; }
        public required String FirstName { get; set; }
        public required String LastName { get; set; }
        public required City City { get; set; }
        public String Address { get; set; } = String.Empty;
        public required String PhoneNumber { get; set; }
        public required String Email { get; set; }
        public required Role Role { get; set; }
    }
}