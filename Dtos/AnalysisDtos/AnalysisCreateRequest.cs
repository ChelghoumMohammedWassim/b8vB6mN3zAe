using b8vB6mN3zAe.Models;
using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Dtos
{
    public class AnalysisCreateRequest
    {
        public required String Ce { get; set; }
        public required String Texture { get; set; }
        public required String Ph { get; set; }
        public required String CN { get; set; }
        public required decimal CActif { get; set; }
        public required decimal OrganicMaterial { get; set; }
        public required decimal Nitrogen { get; set; }
        public required decimal Carbonates { get; set; }
        public required decimal Phosphorus { get; set; }
        public required decimal ExchangeablePotassium { get; set; }
        public required decimal ExchangeableMagnesium { get; set; }
        public required decimal ExchangeableCalcium { get; set; }
        public required decimal ExchangeableSodium { get; set; }
        public required decimal PhosphorusOlsen { get; set; }
        public required decimal ExchangeablepotassiumPPM { get; set; }
        public required decimal ExchangeableMagnesiumPPM { get; set; }
        public required decimal ExchangeableCalciumPPM { get; set; }
        public required decimal ExchangeableSodiumPPM { get; set; }
        public required decimal PhosphorusOlsenPPM { get; set; }
        public required decimal Sand { get; set; }
        public required decimal Clay { get; set; }
        public required decimal Silt { get; set; }
        public required String SampleID { get; set; }
    }
}