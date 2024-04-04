using System.ComponentModel.DataAnnotations;
using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Dtos.UserDtos
{
    public class AdminUsersListResponse
    {
        public String ID{ get; set; } = Guid.NewGuid().ToString();
        public required String UserName { get; set; }
        public required String FirstName { get; set; }
        public required String LastName { get; set; }
        public required CityEnum City { get; set; }
        public String Address { get; set; } = String.Empty;
        public required String PhoneNumber { get; set; }
        public required String Email { get; set; }
        public required Role Role { get; set; }
        public required bool IsActive { get; set; }
        public required String CreatedDate { get; set; }
    }
}