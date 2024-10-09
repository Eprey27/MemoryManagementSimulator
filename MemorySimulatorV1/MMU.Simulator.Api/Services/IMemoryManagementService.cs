// Services/MemoryManagementService.cs
using MMU.Simulator.Api.Models;

namespace MMU.Simulator.Api.Services
{
    public interface IMemoryManagementService
    {
        void ConfigureSimulator(int totalFrames, ReplacementPolicy replacementPolicy, FetchPolicy fetchPolicy, PlacementPolicy placementPolicy);
        void AddProcess(Process process);
        List<Process> GetProcesses();
        PhysicalMemory GetPhysicalMemory();
        SwapSpace GetSwapSpace();
        void RequestPage(int processId, int pageId);
    }
}