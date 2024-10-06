// Models/Process.cs
using System.Collections.Generic;

namespace MMU.Simulator.Api.Models
{
    public class Process
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Segment> Segments { get; set; } // Segmentos del proceso
    }
}
