using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Models
{
    public class RecommendationCreateRequest
    {
        public AdjustGoal Goal { get; set; }
        public required String AnalysisID { get; set; }
        public List<RecommendedFertilizerCreateRequest> RecommendedFertilizers { get; set; } = new List<RecommendedFertilizerCreateRequest>();
    }
}