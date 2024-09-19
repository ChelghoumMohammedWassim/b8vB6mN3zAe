using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Models
{
    public class RecommendationResponse
    {
        public String ID { get; set; } = Guid.NewGuid().ToString();
        public AdjustGoal Goal { get; set; }
        public required String Date { get; set; }
        public required String AnalysisID { get; set; }
        public AnalysisResponse? Analysis { get; set; }
        public List<RecommendedFertilizerResponse?> RecommendedFertilizers { get; set; } = new List<RecommendedFertilizerResponse?>();
    }
}