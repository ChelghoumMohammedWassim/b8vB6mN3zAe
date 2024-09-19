using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Models
{
    public class Recommendation
    {
        public String ID { get; set; } = Guid.NewGuid().ToString();
        public AdjustGoal Goal { get; set; }
        public String Date { get; set; } = DateTime.Now.ToString();
        public required String AnalysisID { get; set; }
        public Analysis? Analysis { get; set;}
        public List<RecommendedFertilizer> RecommendedFertilizers { get; set; } = new List<RecommendedFertilizer> ();
        public required String UserID { get; set; }
        public User? User { get; set; }
    }
}