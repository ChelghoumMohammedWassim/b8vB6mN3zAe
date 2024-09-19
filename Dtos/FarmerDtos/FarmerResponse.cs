using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Dtos
{
    public class FarmerResponse
    {
        public required String ID { get; set; }
        public required String FullName { get; set; }
        public required String Address { get; set; }
        public required String PhoneNumber { get; set; }
        public required String? Email { get; set; }
        public required String NCNA { get; set; }
        public ZipCodeJoinResponse? ZipCode { get; set; }
        public required List<LandJoinResponse?> Lands { get; set; } = new List<LandJoinResponse?>();
    }
}