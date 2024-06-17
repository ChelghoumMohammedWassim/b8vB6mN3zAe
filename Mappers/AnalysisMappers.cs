using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class AnalysisMappers
    {
        

        public static AnalysisResponse? ToAnalysisResponseDto(this Analysis? analysis)
        {
            if (analysis is null)
            {
                return null;
            }
            return new AnalysisResponse
            {
                ID = analysis.ID,
                Ce = analysis.Ce,
                Texture = analysis.Texture,
                Ph = analysis.Ph,
                CN = analysis.CN,
                CActif = analysis.CActif,
                OrganicMaterial = analysis.OrganicMaterial,
                Nitrogen = analysis.Nitrogen,
                Carbonates = analysis.Carbonates,
                Phosphorus = analysis.Phosphorus,
                ExchangeablePotassium = analysis.ExchangeablePotassium,
                ExchangeableMagnesium = analysis.ExchangeableMagnesium,
                ExchangeableCalcium = analysis.ExchangeableCalcium,
                ExchangeableSodium = analysis.ExchangeableSodium,
                PhosphorusOlsen = analysis.PhosphorusOlsen,
                ExchangeablepotassiumPPM = analysis.ExchangeablepotassiumPPM,
                ExchangeableMagnesiumPPM = analysis.ExchangeableMagnesiumPPM,
                ExchangeableCalciumPPM = analysis.ExchangeableCalciumPPM,
                ExchangeableSodiumPPM = analysis.ExchangeableSodiumPPM,
                PhosphorusOlsenPPM = analysis.PhosphorusOlsenPPM,
                Sand = analysis.Sand,
                Clay = analysis.Clay,
                Silt = analysis.Silt,
                Date = analysis.Date,                
            };
        }

    }
}