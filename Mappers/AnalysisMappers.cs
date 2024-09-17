using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class AnalysisMappers
    {


        public static Analysis FromAnalysisCreateRequest(this AnalysisCreateRequest analysisRequest){

            return new Analysis{
                CActif=analysisRequest.CActif,
                Carbonates=analysisRequest.Carbonates,
                Ce=analysisRequest.Ce,
                Clay=analysisRequest.Clay,
                CN=analysisRequest.CN,
                ExchangeableCalcium=analysisRequest.ExchangeableCalcium,
                ExchangeableCalciumPPM=analysisRequest.ExchangeableCalciumPPM,
                ExchangeableMagnesium=analysisRequest.ExchangeableMagnesium,
                ExchangeableMagnesiumPPM=analysisRequest.ExchangeableMagnesiumPPM,
                ExchangeablePotassium=analysisRequest.ExchangeablePotassium,
                ExchangeablepotassiumPPM=analysisRequest.ExchangeablepotassiumPPM,
                ExchangeableSodium=analysisRequest.ExchangeableSodium,
                ExchangeableSodiumPPM=analysisRequest.ExchangeableSodiumPPM,
                Nitrogen=analysisRequest.Nitrogen,
                OrganicMaterial=analysisRequest.OrganicMaterial,
                Ph=analysisRequest.Ph,
                Phosphorus=analysisRequest.Phosphorus,
                PhosphorusOlsen=analysisRequest.PhosphorusOlsen,
                PhosphorusOlsenPPM=analysisRequest.PhosphorusOlsenPPM,
                SampleID=analysisRequest.SampleID,
                Sand=analysisRequest.Sand,
                Silt=analysisRequest.Silt,
                Texture=analysisRequest.Texture,
            };

        }
        

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
                Sample = analysis.Sample.ToSampleJoinResponseDto()       
            };
        }


        public static AnalysisJoinResponse? ToAnalysisJoinResponseDto(this Analysis? analysis)
        {
            if (analysis is null)
            {
                return null;
            }
            return new AnalysisJoinResponse
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