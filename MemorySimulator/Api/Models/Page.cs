// Models/Page.cs
namespace MemorySimulatorBackend.Models
{
    public class Page
    {
        public int Id { get; set; }
        public int PageNumber { get; set; }
        public bool IsValid { get; set; } // Indica si la página está en memoria
        public int? FrameNumber { get; set; } // Número de marco asignado
        public DateTime LastAccessTime { get; set; } // Para LRU
        public DateTime LoadTime { get; set; } // Para FIFO
        public int ProcessId { get; set; } // ID del proceso propietario
    }
}
