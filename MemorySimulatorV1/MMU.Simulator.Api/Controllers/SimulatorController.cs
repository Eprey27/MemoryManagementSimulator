// Controllers/SimulatorController.cs
using Microsoft.AspNetCore.Mvc;
using MMU.Simulator.Api.Models;
using MMU.Simulator.Api.Services;

namespace MMU.Simulator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SimulatorController : ControllerBase
    {
        private readonly IMemoryManagementService _memoryService;

        public SimulatorController(IMemoryManagementService memoryService)
        {
            _memoryService = memoryService;
        }

        [HttpPost("configure")]
        public IActionResult ConfigureSimulator([FromBody] SimulatorConfig config)
        {
            _memoryService.ConfigureSimulator(config);
            return Ok();
        }

        [HttpGet("configuration")]
        public IActionResult GetConfiguration()
        {
            var config = _memoryService.GetConfiguration();
            return Ok(config);
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
        public IActionResult RequestPage([FromBody] PageRequest request)
        {
            try
            {
                _memoryService.RequestPage(request.ProcessId, request.PageId);
                return Ok();
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}

