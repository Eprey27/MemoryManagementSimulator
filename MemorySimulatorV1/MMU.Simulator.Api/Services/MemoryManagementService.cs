// Services/MemoryManagementService.cs
using MMU.Simulator.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MMU.Simulator.Api.Services
{
    public class MemoryManagementService
    {
        private PhysicalMemory _physicalMemory;
        private SwapSpace _swapSpace;
        private List<Process> _processes;
        private ReplacementPolicy _replacementPolicy;
        private FetchPolicy _fetchPolicy;
        private PlacementPolicy _placementPolicy;
        private Queue<Process> _readyQueue;

        public MemoryManagementService(int totalFrames, ReplacementPolicy replacementPolicy, FetchPolicy fetchPolicy, PlacementPolicy placementPolicy)
        {
            _physicalMemory = new PhysicalMemory(totalFrames);
            _swapSpace = new SwapSpace();
            _processes = new List<Process>();
            _replacementPolicy = replacementPolicy;
            _fetchPolicy = fetchPolicy;
            _placementPolicy = placementPolicy;
            _readyQueue = new Queue<Process>();
        }

        public void AddProcess(Process process)
        {
            _processes.Add(process);
            _readyQueue.Enqueue(process);
        }

        public void RequestPage(int processId, int pageId)
        {
            var process = _processes.FirstOrDefault(p => p.Id == processId);
            if (process == null)
            {
                throw new Exception("Proceso no encontrado");
            }

            var page = process.Segments.SelectMany(s => s.Pages)
                                       .FirstOrDefault(p => p.Id == pageId);
            if (page == null)
            {
                throw new Exception("Página no encontrada");
            }

            if (!page.IsValid)
            {
                HandlePageFault(page);

                if (_fetchPolicy == FetchPolicy.Prepaging)
                {
                    PrepageAdjacentPages(process, page);
                }
            }
            else
            {
                page.LastAccessTime = DateTime.Now;
            }
        }

        private void HandlePageFault(Page page)
        {
            var frame = FindFrameForPage(page);
            if (frame != null)
            {
                AssignPageToFrame(page, frame);
            }
            else
            {
                ReplacePage(page);
            }
        }

        private Frame FindFrameForPage(Page page)
        {
            var candidateFrames = _physicalMemory.Frames.Where(f => !f.IsOccupied).ToList();

            switch (_placementPolicy)
            {
                case PlacementPolicy.FirstFit:
                    return candidateFrames.FirstOrDefault();
                case PlacementPolicy.BestFit:
                    // Implementar lógica de mejor ajuste si se considera el tamaño de los marcos
                    return candidateFrames.FirstOrDefault();
                case PlacementPolicy.WorstFit:
                    // Implementar lógica de peor ajuste si se considera el tamaño de los marcos
                    return candidateFrames.FirstOrDefault();
                default:
                    return candidateFrames.FirstOrDefault();
            }
        }

        private void AssignPageToFrame(Page page, Frame frame)
        {
            frame.IsOccupied = true;
            frame.ProcessId = page.ProcessId;
            frame.PageId = page.Id;

            page.IsValid = true;
            page.FrameNumber = frame.FrameNumber;
            page.LastAccessTime = DateTime.Now;
            page.LoadTime = DateTime.Now;
        }

        private void ReplacePage(Page newPage)
        {
            Page pageToReplace = null;

            switch (_replacementPolicy)
            {
                case ReplacementPolicy.FIFO:
                    pageToReplace = SelectPageToReplaceFIFO();
                    break;
                case ReplacementPolicy.LRU:
                    pageToReplace = SelectPageToReplaceLRU();
                    break;
                case ReplacementPolicy.Optimal:
                    // Implementación del algoritmo óptimo requiere conocimiento futuro
                    pageToReplace = SelectPageToReplaceLRU(); // Placeholder
                    break;
            }

            var frame = _physicalMemory.Frames.First(f => f.FrameNumber == pageToReplace.FrameNumber);

            pageToReplace.IsValid = false;
            pageToReplace.FrameNumber = null;

            AssignPageToFrame(newPage, frame);
        }

        private Page SelectPageToReplaceFIFO()
        {
            var pagesInMemory = _processes.SelectMany(p => p.Segments)
                                          .SelectMany(s => s.Pages)
                                          .Where(p => p.IsValid)
                                          .OrderBy(p => p.LoadTime)
                                          .ToList();

            return pagesInMemory.First();
        }

        private Page SelectPageToReplaceLRU()
        {
            var pagesInMemory = _processes.SelectMany(p => p.Segments)
                                          .SelectMany(s => s.Pages)
                                          .Where(p => p.IsValid)
                                          .OrderBy(p => p.LastAccessTime)
                                          .ToList();

            return pagesInMemory.First();
        }

        private void PrepageAdjacentPages(Process process, Page currentPage)
        {
            var adjacentPages = GetAdjacentPages(process, currentPage);
            foreach (var page in adjacentPages)
            {
                if (!page.IsValid)
                {
                    HandlePageFault(page);
                }
            }
        }

        private List<Page> GetAdjacentPages(Process process, Page currentPage)
        {
            var pages = process.Segments.SelectMany(s => s.Pages).ToList();
            int index = pages.FindIndex(p => p.Id == currentPage.Id);
            var adjacentPages = new List<Page>();

            if (index + 1 < pages.Count)
            {
                adjacentPages.Add(pages[index + 1]);
            }

            return adjacentPages;
        }

        public void SwapOutProcess(int processId)
        {
            var process = _processes.FirstOrDefault(p => p.Id == processId);
            if (process != null)
            {
                foreach (var page in process.Segments.SelectMany(s => s.Pages))
                {
                    if (page.IsValid)
                    {
                        var frame = _physicalMemory.Frames.First(f => f.FrameNumber == page.FrameNumber);
                        frame.IsOccupied = false;
                        frame.ProcessId = null;
                        frame.PageId = null;

                        page.IsValid = false;
                        page.FrameNumber = null;

                        _swapSpace.Pages.Add(page);
                    }
                }
            }
        }

        public void SwapInProcess(int processId)
        {
            var pagesInSwap = _swapSpace.Pages.Where(p => p.ProcessId == processId).ToList();
            foreach (var page in pagesInSwap)
            {
                HandlePageFault(page);
                _swapSpace.Pages.Remove(page);
            }
        }

        public PhysicalMemory GetPhysicalMemory()
        {
            return _physicalMemory;
        }

        public SwapSpace GetSwapSpace()
        {
            return _swapSpace;
        }

        public List<Process> GetProcesses()
        {
            return _processes;
        }
    }
}
