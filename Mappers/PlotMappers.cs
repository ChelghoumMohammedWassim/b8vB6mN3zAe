using b8vB6mN3zAe.Dtos;
using b8vB6mN3zAe.Models;

namespace b8vB6mN3zAe.Mappers
{
    public static class PlotMappers
    {
        public static Plot FromCreatePlotRequestDto(this CreatePlotRequest plotRequest)
        {
            return new Plot
            {
                Name = plotRequest.Name,
                Polygon = plotRequest.Polygon,
                Surface = plotRequest.Surface,
                Production = plotRequest.Production,
                TreeAge = plotRequest.TreeAge,
                Width = plotRequest.Width,
                Type = plotRequest.Type,
                ExploitationID = plotRequest.ExploitationID,
                Length = plotRequest.Length,
            };
        }

        public static PlotResponse? ToPlotResponseDto(this Plot? plot)
        {
            if (plot is null)
            {
                return null;
            }
            return new PlotResponse{
                ID = plot.ID,
                Name = plot.Name,
                Polygon = plot.Polygon,
                Surface = plot.Surface,
                Production = plot.Production,
                TreeAge = plot.TreeAge,
                Width = plot.Width,
                Length = plot.Length,
                Type = plot.Type,
                Exploitation = plot.Exploitation.ToExploitationJoinResponseDto(),
                Positions = plot.Positions.Select(p=> p.ToPositionResponseDto()).ToList(),
                Samples = plot.Samples.Select(p=> p.ToSampleJoinResponseDto()).ToList(),
            };
        }

        public static PlotJoinResponse? ToJoinPlotResponseDto(this Plot? plot)
        {
            if (plot is null)
            {
                return null;
            }
            return new PlotJoinResponse{
                ID = plot.ID,
                Name = plot.Name,
                Polygon = plot.Polygon,
                Surface = plot.Surface,
                Production = plot.Production,
                TreeAge = plot.TreeAge,
                Width = plot.Width,
                Length = plot.Length,
                Type = plot.Type,
                Positions = plot.Positions.Select(p=> p.ToPositionResponseDto()).ToList(),
            };
        }
    }
}