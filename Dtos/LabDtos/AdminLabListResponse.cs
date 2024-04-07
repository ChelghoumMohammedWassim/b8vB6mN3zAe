using b8vB6mN3zAe.Dtos.City;
using b8vB6mN3zAe.Models;
using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Dtos.UserDtos
{
    public class AdminLabListResponse
    {
        public String ID { get; set; } = String.Empty;
        public required String UserName { get; set; }
        public required String Name { get; set; }
        public required CityJoinResponse? City { get; set; }
        public String Address { get; set; } = String.Empty;
        public required String PhoneNumber { get; set; }
        public required String Email { get; set; }
        public bool IsActive { get; set; } = true;
        public required String CreatedDate { get; set; }
        public List<SectorJoinResponse> Sectors { get; set; } = new List<SectorJoinResponse>();
    }
}