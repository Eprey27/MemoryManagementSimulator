// Models/Frame.cs
namespace MemorySimulatorBackend.Models
{
    public class Frame
    {
        public int FrameNumber { get; set; }
        public bool IsOccupied { get; set; }
        public int? ProcessId { get; set; }
        public int? PageId { get; set; }
    }
}
