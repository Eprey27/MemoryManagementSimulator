// Controllers/SimulatorController.cs
using Microsoft.AspNetCore.Mvc;
using MemorySimulatorBackend.Models;
using MemorySimulatorBackend.Services;

namespace MemorySimulatorBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SimulatorController : ControllerBase
    {
        private readonly MemoryManagementService _memoryService;

        public SimulatorController(MemoryManagementService memoryService)
        {
            _memoryService = memoryService;
        }

        [HttpGet("memory")]
        public IActionResult GetMemoryState()
        {
            var memory = _memoryService.GetPhysicalMemory();
            return Ok(memory);
        }

        [HttpGet("swap")]
        public IActionResult GetSwapSpace()
        {
            var swapSpace = _memoryService.GetSwapSpace();
            return Ok(swapSpace);
        }

        [HttpPost("requestPage")]
        public IActionResult RequestPage(int processId, int pageId)
        {
            _memoryService.RequestPage(processId, pageId);
            return Ok();
        }
    }
}
