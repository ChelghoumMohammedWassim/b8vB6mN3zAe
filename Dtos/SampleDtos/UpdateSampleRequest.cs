using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace b8vB6mN3zAe.Dtos
{
    public class UpdateSampleRequest
    {
        public required String ID { get; set; }
        public required String Reference { get; set; }
        public required String SamplingDate { get; set; }       
        public required String PlotID { get; set; }

    }
}