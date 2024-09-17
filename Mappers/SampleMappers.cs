using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class SampleMappers
    {
        public static Sample FromCreateSampleRequestDto(this CreateSampleRequest sampleRequest)
        {
            return new Sample
            {
                Reference= sampleRequest.Reference,
                SamplingDate= sampleRequest.SamplingDate.ToString(),
                PlotID= sampleRequest.PlotID,
            };
        }

        public static SampleResponse? ToSampleResponseDto(this Sample? sample)
        {
            if (sample is null)
            {
                return null;
            }
            return new SampleResponse
            {
                ID = sample.ID,
                Reference = sample.Reference,
                SamplingDate = sample.SamplingDate,
                Status = sample.Status.ToString(),
                Analyses = sample.Analyses.Select(x => x.ToAnalysisResponseDto()).ToList(),
                Plot = sample.Plot.ToJoinPlotResponseDto()
            };
        }


        public static SampleJoinResponse? ToSampleJoinResponseDto(this Sample? sample)
        {
            if (sample is null)
            {
                return null;
            }
            return new SampleJoinResponse
            {
                ID = sample.ID,
                Reference = sample.Reference,
                SamplingDate = sample.SamplingDate,
                Status = sample.Status.ToString()

            };
        }
    }
}