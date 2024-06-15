using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Models
{
    public class Sample
    {
        public String ID { get; set; }= Guid.NewGuid().ToString();
        public required String Reference { get; set; }
        public required String SamplingDate { get; set; }
        public SampleStatus Status { get; set; } = SampleStatus.registered;
       
        public required String PlotID { get; set; }
        public Plot? Plot { get; set; }
        public List<Analysis> Analyses { get; set; } = new List<Analysis>();
    }
}