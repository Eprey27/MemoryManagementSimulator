// Controllers/SimulatorController.cs
using Microsoft.AspNetCore.Mvc;
using MMU.Simulator.Api.Services;
using MMU.Simulator.Api.Models;

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
            _memoryService.ConfigureSimulator(config.TotalFrames, config.ReplacementPolicy, config.FetchPolicy, config.PlacementPolicy);
            return Ok();
        }

        [HttpGet("configuration")]
        public IActionResult GetConfiguration()
        {
            // Si tienes un método para obtener la configuración actual
            // var config = _memoryService.GetConfiguration();
            // return Ok(config);

            // Si no, puedes devolver un mensaje
            return Ok(new { Message = "Configuración actual no disponible." });
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
