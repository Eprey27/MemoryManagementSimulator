namespace MMU.Simulator.Api.Models
{
    public class SimulatorConfig
    {
        public int TotalFrames { get; set; }
        public string ReplacementPolicy { get; set; }
        public string FetchPolicy { get; set; }
        public string PlacementPolicy { get; set; }
    }
}
