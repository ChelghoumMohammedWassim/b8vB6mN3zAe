using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Dtos
{
    public class AnalysisStatusUpdateRequest
    {
        public required String AnalyzeID { get; set; }
        public required AnalysisStatus AnalysisStatus { get; set; }
    }
}