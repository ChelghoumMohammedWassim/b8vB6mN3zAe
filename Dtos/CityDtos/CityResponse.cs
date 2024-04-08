using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Dtos
{
    public class CityResponse
    {
        public int ID { get; set; }
        public required String Name { get; set; }

        public SectorJoinResponse? Sector { get; set; } = null;
        public List<ZipCodeJoinResponse> ZipCodes { get; set; } = new List<ZipCodeJoinResponse>();
    }
}