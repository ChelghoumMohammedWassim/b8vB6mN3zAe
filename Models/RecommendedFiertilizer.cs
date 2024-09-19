
namespace b8vB6mN3zAe.Models
{
    public class RecommendedFertilizer
    {
        public String ID { get; set; } = Guid.NewGuid().ToString();
        public required float Quantity { get; set; }
        public required String FertilizerID { get; set; }
        public  Fertilizer? Fertilizer { get; set; }
        public required String RecommendationID { get; set; }
        public  Recommendation? Recommendation { get; set; }
    }
}