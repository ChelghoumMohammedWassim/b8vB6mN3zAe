using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Dtos
{
    public class CreateSampleRequest
    {
        public required String Reference { get; set; }
        public required DateTime SamplingDate { get; set; }       
        public required String PlotID { get; set; }
    }
}