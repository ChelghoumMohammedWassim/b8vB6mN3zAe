using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Dtos
{
    public class FarmerResponse
    {
        public required String ID { get; set; }
        public required String FirstName { get; set; }
        public required String LastName { get; set; }
        public required String Address { get; set; }
        public required String PhoneNumber { get; set; }
        public required String? Email { get; set; }
        public required String NCNA { get; set; }
        public ZipCodeJoinResponse? ZipCode { get; set; }
    }
}