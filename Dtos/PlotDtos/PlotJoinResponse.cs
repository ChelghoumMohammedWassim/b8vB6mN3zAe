using b8vB6mN3zAe.Models;
using b8vB6mN3zAe.Models.Enums;

namespace b8vB6mN3zAe.Dtos
{
    public class PlotJoinResponse
    {
        public String ID { get; set; }= Guid.NewGuid().ToString();
        public required String Name { get; set; }
        public required int Polygon { get; set; }
        public required decimal Surface { get; set; }
        public required decimal Production { get; set; }
        public required int TreeAge { get; set; }
        public required decimal Width { get; set; }
        public required decimal Length { get; set; }
        public required ExploitationType Type { get; set; }
        public List<PostionResponse> Positions { get; set; } = new List<PostionResponse>();
    }
}