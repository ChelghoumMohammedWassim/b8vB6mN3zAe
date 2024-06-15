using b8vB6mN3zAe.Models;
using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Dtos
{
    public class AnalysisResponse
    {
        public String ID { get; set; }= Guid.NewGuid().ToString();
        public required String InterpretationMethod { get; set; }
        public required String InterpretationRule { get; set; }
        public required String Ce { get; set; }
        public required String Texture { get; set; }
        public required String Ph { get; set; }
        public required String CN { get; set; }
        public required decimal CaCo3 { get; set; }
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
        public required String Date { get; set; }
    }
}