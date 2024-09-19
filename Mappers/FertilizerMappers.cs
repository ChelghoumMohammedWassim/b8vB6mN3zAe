using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class FertilizerMappers
    {
        public static Fertilizer FromCreateFertilizerRequestDto(this FertilizerCreateRequest fertilizerRequest)
        {
            return new Fertilizer
            {
                Name = fertilizerRequest.Name,
                N = fertilizerRequest.N,
                Ammoniacal = fertilizerRequest.Ammoniacal,
                Ureic = fertilizerRequest.Ureic,
                Nitric = fertilizerRequest.Nitric,
                P2O5 = fertilizerRequest.P2O5,
                K2O = fertilizerRequest.K2O,
                MgO = fertilizerRequest.MgO,
                CaO = fertilizerRequest.CaO,
                Fe = fertilizerRequest.Fe,
                Zn = fertilizerRequest.Zn,
                Mn = fertilizerRequest.Mn,
                S = fertilizerRequest.S,
                Cl = fertilizerRequest.Cl,
                Density = fertilizerRequest.Density,
                Solubility = fertilizerRequest.Solubility,
                ConductivityMax = fertilizerRequest.ConductivityMax,
                ReactionType = fertilizerRequest.ReactionType,
                FertilizerType = fertilizerRequest.FertilizerType,
                SubType = fertilizerRequest.SubType,
            };
        }

        public static FertilizerResponse? ToFertilizerResponseDto(this Fertilizer? fertilizer)
        {
            if (fertilizer is null)
            {
                return null;
            }
            return new FertilizerResponse
            {
                ID = fertilizer.ID,
                Name = fertilizer.Name,
                N = fertilizer.N,
                Ammoniacal = fertilizer.Ammoniacal,
                Ureic = fertilizer.Ureic,
                Nitric = fertilizer.Nitric,
                P2O5 = fertilizer.P2O5,
                K2O = fertilizer.K2O,
                MgO = fertilizer.MgO,
                CaO = fertilizer.CaO,
                Fe = fertilizer.Fe,
                Zn = fertilizer.Zn,
                Mn = fertilizer.Mn,
                S = fertilizer.S,
                Cl = fertilizer.Cl,
                Density = fertilizer.Density,
                Solubility = fertilizer.Solubility,
                ConductivityMax = fertilizer.ConductivityMax,
                ReactionType = fertilizer.ReactionType,
                FertilizerType = fertilizer.FertilizerType,
                SubType = fertilizer.SubType,
            };
        }

    }
}