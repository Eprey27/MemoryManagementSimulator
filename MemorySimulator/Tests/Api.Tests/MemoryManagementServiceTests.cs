// Tests/MemoryManagementServiceTests.cs
using FluentAssertions;
using MemorySimulatorBackend.Models;
using MemorySimulatorBackend.Services;

namespace MemorySimulatorBackend.Tests
{
    [TestFixture]
    public class MemoryManagementServiceTests
    {
        private MemoryManagementService _memoryService;

        [SetUp]
        public void Setup()
        {
            int totalFrames = 10;
            _memoryService = new MemoryManagementService(
                totalFrames,
                ReplacementPolicy.LRU,
                FetchPolicy.DemandPaging,
                PlacementPolicy.FirstFit
            );
        }

        [Test]
        public void AddProcess_Should_Add_Process_To_List()
        {
            // Arrange
            var process = new Process
            {
                Id = 1,
                Name = "Proceso 1",
                Segments = new List<Segment>()
            };

            // Act
            _memoryService.AddProcess(process);

            // Assert
            var processes = _memoryService.GetProcesses();
            processes.Should().Contain(process);
        }

        [Test]
        public void RequestPage_Should_Handle_Page_Fault()
        {
            // Arrange
            var page = new Page { Id = 1, PageNumber = 0, ProcessId = 1 };
            var segment = new Segment { Id = 1, Size = 1, Pages = new List<Page> { page } };
            var process = new Process
            {
                Id = 1,
                Name = "Proceso 1",
                Segments = new List<Segment> { segment }
            };

            _memoryService.AddProcess(process);

            // Act
            _memoryService.RequestPage(1, 1);

            // Assert
            page.IsValid.Should().BeTrue();
            page.FrameNumber.Should().NotBeNull();
        }

        [Test]
        public void SwapOutProcess_Should_Move_Pages_To_SwapSpace()
        {
            // Arrange
            var page = new Page { Id = 1, PageNumber = 0, ProcessId = 1 };
            var segment = new Segment { Id = 1, Size = 1, Pages = new List<Page> { page } };
            var process = new Process
            {
                Id = 1,
                Name = "Proceso 1",
                Segments = new List<Segment> { segment }
            };

            _memoryService.AddProcess(process);
            _memoryService.RequestPage(1, 1);

            // Act
            _memoryService.SwapOutProcess(1);

            // Assert
            page.IsValid.Should().BeFalse();
            var swapSpace = _memoryService.GetSwapSpace();
            swapSpace.Pages.Should().Contain(page);
        }
    }
}
