// Models/PhysicalMemory.cs
using System.Collections.Generic;

namespace MMU.Simulator.Api.Models
{
    public class PhysicalMemory
    {
        public int TotalFrames { get; set; }
        public List<Frame> Frames { get; set; }
    }
}

