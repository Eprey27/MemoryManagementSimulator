// Models/PhysicalMemory.cs
using System.Collections.Generic;

namespace MemorySimulatorBackend.Models
{
    public class PhysicalMemory
    {
        public int TotalFrames { get; set; }
        public List<Frame> Frames { get; set; }

        public PhysicalMemory(int totalFrames)
        {
            TotalFrames = totalFrames;
            Frames = new List<Frame>();
            for (int i = 0; i < totalFrames; i++)
            {
                Frames.Add(new Frame { FrameNumber = i, IsOccupied = false });
            }
        }
    }
}
