// Services/MemoryManagementService.cs
using MMU.Simulator.Api.Models;

namespace MMU.Simulator.Api.Services
{
    public class MemoryManagementService : IMemoryManagementService
    {
        private int _totalFrames;
        private ReplacementPolicy _replacementPolicy;
        private FetchPolicy _fetchPolicy;
        private PlacementPolicy _placementPolicy;

        private PhysicalMemory _physicalMemory;
        private SwapSpace _swapSpace;
        private List<Process> _processes;

        public MemoryManagementService()
        {
            // Inicializar con valores predeterminados o vacíos
            _totalFrames = 100;
            _replacementPolicy = ReplacementPolicy.LRU;
            _fetchPolicy = FetchPolicy.DemandPaging;
            _placementPolicy = PlacementPolicy.FirstFit;

            _physicalMemory = new PhysicalMemory
            {
                TotalFrames = _totalFrames,
                Frames = new List<Frame>()
            };

            _swapSpace = new SwapSpace
            {
                Pages = new List<Page>()
            };

            _processes = new List<Process>();

            InitializePhysicalMemory();
        }

        public void ConfigureSimulator(int totalFrames, ReplacementPolicy replacementPolicy, FetchPolicy fetchPolicy, PlacementPolicy placementPolicy)
        {
            _totalFrames = totalFrames;
            _replacementPolicy = replacementPolicy;
            _fetchPolicy = fetchPolicy;
            _placementPolicy = placementPolicy;

            // Reiniciar y reconfigurar la memoria física
            _physicalMemory.TotalFrames = _totalFrames;
            _physicalMemory.Frames.Clear();
            InitializePhysicalMemory();

            // Reiniciar el espacio de intercambio y los procesos
            _swapSpace.Pages.Clear();
            _processes.Clear();
        }

        private void InitializePhysicalMemory()
        {
            for (int i = 0; i < _totalFrames; i++)
            {
                _physicalMemory.Frames.Add(new Frame
                {
                    FrameNumber = i,
                    IsOccupied = false
                });
            }
        }

        public void AddProcess(Process process)
        {
            _processes.Add(process);

            // Agregar todas las páginas al espacio de intercambio
            foreach (var segment in process.Segments)
            {
                foreach (var page in segment.Pages)
                {
                    page.ProcessId = process.Id;
                    _swapSpace.Pages.Add(page);
                }
            }
        }

        public List<Process> GetProcesses()
        {
            return _processes;
        }

        public PhysicalMemory GetPhysicalMemory()
        {
            return _physicalMemory;
        }

        public SwapSpace GetSwapSpace()
        {
            return _swapSpace;
        }

        public void RequestPage(int processId, int pageId)
        {
            // Implementar la lógica para manejar la solicitud de página
            // Incluyendo las políticas configuradas

            // Ejemplo simplificado:
            var page = _swapSpace.Pages.FirstOrDefault(p => p.ProcessId == processId && p.Id == pageId);

            if (page != null)
            {
                var freeFrame = _physicalMemory.Frames.FirstOrDefault(f => !f.IsOccupied);

                if (freeFrame != null)
                {
                    // Cargar la página en la memoria física
                    freeFrame.IsOccupied = true;
                    freeFrame.ProcessId = processId;
                    freeFrame.PageId = pageId;

                    page.IsValid = true;
                    page.FrameNumber = freeFrame.FrameNumber;
                    page.LoadTime = DateTime.Now;
                    page.LastAccessTime = DateTime.Now;

                    _swapSpace.Pages.Remove(page);
                }
                else
                {
                    // Aplicar la política de reemplazo
                    ApplyReplacementPolicy(page);
                }
            }
            else
            {
                throw new Exception("La página solicitada no se encuentra en el espacio de intercambio.");
            }
        }

        private void ApplyReplacementPolicy(Page newPage)
        {
            // Implementar la lógica según _replacementPolicy
            // Este es un ejemplo simplificado usando FIFO

            var frameToReplace = _physicalMemory.Frames.FirstOrDefault(f => f.IsOccupied);

            if (frameToReplace != null)
            {
                // Encontrar la página en memoria
                var pageInMemory = _processes
                    .SelectMany(p => p.Segments)
                    .SelectMany(s => s.Pages)
                    .FirstOrDefault(p => p.FrameNumber == frameToReplace.FrameNumber);

                if (pageInMemory != null)
                {
                    // Mover la página al espacio de intercambio
                    pageInMemory.IsValid = false;
                    pageInMemory.FrameNumber = null;
                    pageInMemory.LastAccessTime = DateTime.Now;

                    _swapSpace.Pages.Add(pageInMemory);
                }

                // Cargar la nueva página en el marco reemplazado
                frameToReplace.ProcessId = newPage.ProcessId;
                frameToReplace.PageId = newPage.Id;

                newPage.IsValid = true;
                newPage.FrameNumber = frameToReplace.FrameNumber;
                newPage.LoadTime = DateTime.Now;
                newPage.LastAccessTime = DateTime.Now;
                            
                _swapSpace.Pages.Remove(newPage);
            }
            else
            {
                throw new Exception("No se pudo aplicar la política de reemplazo. No hay marcos ocupados.");
            }
        }

        // Otros métodos y funcionalidades existentes
    }
}
