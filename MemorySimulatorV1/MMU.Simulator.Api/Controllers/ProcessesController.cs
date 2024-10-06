// Controllers/ProcessesController.cs
using Microsoft.AspNetCore.Mvc;
using MMU.Simulator.Api.Models;
using MMU.Simulator.Api.Services;

namespace MMU.Simulator.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcessesController : ControllerBase
    {
        private readonly MemoryManagementService _memoryService;

        public ProcessesController(MemoryManagementService memoryService)
        {
            _memoryService = memoryService;
        }

        [HttpPost]
        public IActionResult AddProcess([FromBody] Process process)
        {
            _memoryService.AddProcess(process);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetProcesses()
        {
            var processes = _memoryService.GetProcesses();
            return Ok(processes);
        }
    }
}
