using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Models
{
    public class FertilizerCreateRequest
    {
        public required String Name { get; set; }
        public required decimal N { get; set; }
        public required decimal Ammoniacal { get; set; }
        public required decimal Ureic { get; set; }
        public required decimal Nitric { get; set; }
        public required decimal P2O5 { get; set; }
        public required decimal K2O { get; set; }
        public required decimal MgO { get; set; }
        public required decimal CaO { get; set; }
        public required decimal Fe { get; set; }
        public required decimal Zn { get; set; }
        public required decimal Mn { get; set; }
        public required decimal S { get; set; }
        public required decimal Cl { get; set; }
        public required int Density { get; set; }
        public required int Solubility { get; set; }
        public required int ConductivityMax { get; set; }
        public required ReactionType ReactionType { get; set; }
        public required FertilizerType FertilizerType { get; set; }
        public required FertilizerSubType SubType { get; set; }
    }
}