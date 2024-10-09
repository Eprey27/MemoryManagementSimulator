namespace MMU.Simulator.Api.Models
{
    public class SimulatorConfig
    {
        public int TotalFrames { get; set; }
        public ReplacementPolicy ReplacementPolicy { get; set; }
        public FetchPolicy FetchPolicy { get; set; }
        public PlacementPolicy PlacementPolicy { get; set; }
    }
}
