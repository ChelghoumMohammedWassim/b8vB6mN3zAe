using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Dtos
{
    public class SampleResponse
    {
        public String ID { get; set; }= Guid.NewGuid().ToString();
        public required String Reference { get; set; }
        public required String SamplingDate { get; set; }
        public required String Status { get; set; }
        public List<AnalysisResponse?> Analyses { get; set; } = new List<AnalysisResponse?>();

    }
}