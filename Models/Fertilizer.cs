using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Models
{
    public class Fertilizer
    {
        public String ID { get; set; } = Guid.NewGuid().ToString();
        public String Name { get; set; } = String.Empty;
        public decimal N { get; set; }
        public decimal Ammoniacal { get; set; }
        public decimal Ureic { get; set; }
        public decimal Nitric { get; set; }
        public decimal P2O5 { get; set; }
        public decimal K2O { get; set; }
        public decimal MgO { get; set; }
        public decimal CaO { get; set; }
        public decimal Fe { get; set; }
        public decimal Zn { get; set; }
        public decimal Mn { get; set; }
        public decimal S { get; set; }
        public decimal Cl { get; set; }
        public int Density { get; set; }
        public int Solubility { get; set; }
        public int ConductivityMax { get; set; }
        public ReactionType ReactionType { get; set; }
        public FertilizerType FertilizerType { get; set; }
        public FertilizerSubType SubType { get; set; }
        public List<RecommendedFertilizer> RecommendedFertilizers { get; set; } = new List<RecommendedFertilizer> ();
    }
}