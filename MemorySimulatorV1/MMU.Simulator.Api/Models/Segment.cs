// Models/Segment.cs
using System.Collections.Generic;

namespace MMU.Simulator.Api.Models
{
    public class Segment
    {
        public int Id { get; set; }
        public int Size { get; set; } // Tamaño del segmento
        public List<Page> Pages { get; set; } // Páginas que conforman el segmento
    }
}
