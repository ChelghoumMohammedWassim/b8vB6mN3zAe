
using b8vB6mN3zAe.Dtos.City;
using b8vB6mN3zAe.Models;
using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Dtos.UserDtos
{
    public class AdminUsersListResponse
    {
        public String ID{ get; set; } = Guid.NewGuid().ToString();
        public required String UserName { get; set; }
        public required String FirstName { get; set; }
        public required String LastName { get; set; }
        public CityJoinResponse? City { get; set; }
        public String Address { get; set; } = String.Empty;
        public required String PhoneNumber { get; set; }
        public required String Email { get; set; }
        public required String Role { get; set; }
        public required bool IsActive { get; set; }
        public required List<UserSectorResponse>? Sectors { get; set; }= new List<UserSectorResponse>();
        public required String CreatedDate { get; set; }
    }
}