using FluentAssertions;
using MMU.Simulator.Api.Models;
using MMU.Simulator.Api.Services;

namespace MMU.Simulator.Api.Service.Tests
{
    public class MemoryManagementServiceTests
    {
        private MemoryManagementService _memoryService;

        [SetUp]
        public void Setup()
        {
            // Inicializamos el servicio real para pruebas integrales
            _memoryService = new MemoryManagementService();

            // Configuramos el simulador
            _memoryService.ConfigureSimulator(
                totalFrames: 10,
                replacementPolicy: ReplacementPolicy.LRU,
                fetchPolicy: FetchPolicy.DemandPaging,
                placementPolicy: PlacementPolicy.FirstFit
            );
        }

        [Test]
        public void AddProcess__Should_Add__Process_To_List()
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
        public void AddProcess__Should_Throw__Argument_Null_Exception__When__Process_Is_Null()
        {
            // Arrange
            Process nullProcess = null;

            // Act
            Action act = () => _memoryService.AddProcess(nullProcess);

            // Assert
            act.Should().Throw<ArgumentNullException>().WithMessage("Value cannot be null. (Parameter 'process')");
        }

        [Test]
        public void RequestPage__Should_Load_Page_Into_PhysicalMemory__When__Frames_Are_Available()
        {
            // Arrange
            var page = new Page { Id = 1, PageNumber = 0 };
            var segment = new Segment { Id = 1, Size = 1, Pages = new List<Page> { page } };
            var process = new Process
            {
                Id = 1,
                Name = "Proceso 1",
                Segments = new List<Segment> { segment }
            };

            _memoryService.AddProcess(process);

            // Act
            _memoryService.RequestPage(process.Id, page.Id);
            var physicalMemory = _memoryService.GetPhysicalMemory();

            // Assert
            page.IsValid.Should().BeTrue();
            page.FrameNumber.Should().NotBeNull();
            physicalMemory.Frames.Should().ContainSingle(f => f.IsOccupied && f.ProcessId == process.Id && f.PageId == page.Id);
        }

        [Test]
        public void RequestPage__Should_Apply_ReplacementPolicy__When__No_Available_Frames()
        {
            // Arrange
            _memoryService.ConfigureSimulator(
                totalFrames: 1,
                replacementPolicy: ReplacementPolicy.FIFO,
                fetchPolicy: FetchPolicy.DemandPaging,
                placementPolicy: PlacementPolicy.FirstFit
            );

            var page1 = new Page { Id = 1, PageNumber = 0 };
            var segment1 = new Segment { Id = 1, Size = 1, Pages = new List<Page> { page1 } };
            var process1 = new Process
            {
                Id = 1,
                Name = "Proceso 1",
                Segments = new List<Segment> { segment1 }
            };
            _memoryService.AddProcess(process1);
            _memoryService.RequestPage(process1.Id, page1.Id);

            var page2 = new Page { Id = 2, PageNumber = 0 };
            var segment2 = new Segment { Id = 1, Size = 1, Pages = new List<Page> { page2 } };
            var process2 = new Process
            {
                Id = 2,
                Name = "Proceso 2",
                Segments = new List<Segment> { segment2 }
            };
            _memoryService.AddProcess(process2);

            // Act
            _memoryService.RequestPage(process2.Id, page2.Id);

            // Assert
            page2.IsValid.Should().BeTrue();
            page2.FrameNumber.Should().NotBeNull();

            page1.IsValid.Should().BeFalse();
            page1.FrameNumber.Should().BeNull();

            var swapSpace = _memoryService.GetSwapSpace();
            swapSpace.Pages.Should().Contain(page1);
        }

        [Test]
        public void RequestPage__Should_Throw__Exception__When__Page_Does_Not_Exist()
        {
            // Arrange
            var processId = 1;
            var pageId = 999; // Página que no existe

            // Act
            Action act = () => _memoryService.RequestPage(processId, pageId);

            // Assert
            act.Should().Throw<Exception>().WithMessage("La página solicitada no se encuentra en el espacio de intercambio.");
        }

        [Test]
        public void ConfigureSimulator__Should_Reset__Internal_State()
        {
            // Arrange
            var page = new Page { Id = 1, PageNumber = 0 };
            var segment = new Segment { Id = 1, Size = 1, Pages = new List<Page> { page } };
            var process = new Process
            {
                Id = 1,
                Name = "Proceso 1",
                Segments = new List<Segment> { segment }
            };
            _memoryService.AddProcess(process);
            _memoryService.RequestPage(process.Id, page.Id);

            // Act
            _memoryService.ConfigureSimulator(
                totalFrames: 5,
                replacementPolicy: ReplacementPolicy.Optimal,
                fetchPolicy: FetchPolicy.Prepaging,
                placementPolicy: PlacementPolicy.BestFit
            );

            // Assert
            var processes = _memoryService.GetProcesses();
            processes.Should().BeEmpty();

            var physicalMemory = _memoryService.GetPhysicalMemory();
            physicalMemory.TotalFrames.Should().Be(5);
            physicalMemory.Frames.Should().OnlyContain(f => !f.IsOccupied);

            var swapSpace = _memoryService.GetSwapSpace();
            swapSpace.Pages.Should().BeEmpty();
        }

        [Test]
        public void GetSwapSpace__Should_Return__Pages_In_Swap()
        {
            // Arrange
            var page1 = new Page { Id = 1, PageNumber = 0 };
            var segment1 = new Segment { Id = 1, Size = 1, Pages = new List<Page> { page1 } };
            var process1 = new Process
            {
                Id = 1,
                Name = "Proceso 1",
                Segments = new List<Segment> { segment1 }
            };
            _memoryService.AddProcess(process1);

            var page2 = new Page { Id = 2, PageNumber = 0 };
            var segment2 = new Segment { Id = 1, Size = 1, Pages = new List<Page> { page2 } };
            var process2 = new Process
            {
                Id = 2,
                Name = "Proceso 2",
                Segments = new List<Segment> { segment2 }
            };
            _memoryService.AddProcess(process2);

            // Solo cargamos la página del primer proceso
            _memoryService.RequestPage(process1.Id, page1.Id);

            // Act
            var swapSpace = _memoryService.GetSwapSpace();

            // Assert
            swapSpace.Pages.Should().Contain(page2);
            swapSpace.Pages.Should().NotContain(page1);
        }
    }
}
