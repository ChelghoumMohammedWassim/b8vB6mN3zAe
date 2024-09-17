using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Dtos
{
    public class SampleJoinResponse
    {
        public String ID { get; set; }= Guid.NewGuid().ToString();
        public required String Reference { get; set; }
        public required String SamplingDate { get; set; }
        public required String Status { get; set; }
    }
}