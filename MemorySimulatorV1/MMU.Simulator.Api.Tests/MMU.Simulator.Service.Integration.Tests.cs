// Tests/MemoryManagementServiceTests.cs
using FluentAssertions;
using MMU.Simulator.Api.Models;
using MMU.Simulator.Api.Services;

namespace MMU.Simulator.Api.Service.Integration.Tests
{
    [TestFixture]
    public class MemoryManagementServiceTests
    {
        private IMemoryManagementService _memoryService;

        [SetUp]
        public void Setup()
        {
            // Inicializamos el servicio sin parámetros, ya que ahora las políticas se configuran posteriormente
            _memoryService = new MemoryManagementService();

            // Configuramos el simulador con valores específicos para las pruebas
            _memoryService.ConfigureSimulator(
                totalFrames: 10,
                replacementPolicy: ReplacementPolicy.LRU,
                fetchPolicy: FetchPolicy.DemandPaging,
                placementPolicy: PlacementPolicy.FirstFit
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
        public void RequestPage_Should_Load_Page_Into_PhysicalMemory()
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

            // Assert
            page.IsValid.Should().BeTrue();
            page.FrameNumber.Should().NotBeNull();

            var physicalMemory = _memoryService.GetPhysicalMemory();
            var frame = physicalMemory.Frames.Find(f => f.FrameNumber == page.FrameNumber);

            frame.Should().NotBeNull();
            frame.IsOccupied.Should().BeTrue();
            frame.ProcessId.Should().Be(process.Id);
            frame.PageId.Should().Be(page.Id);
        }

        [Test]
        public void RequestPage_Should_Handle_PageFault_When_No_FreeFrames_Available()
        {
            // Arrange
            // Configuramos el simulador con un número limitado de marcos
            _memoryService.ConfigureSimulator(
                totalFrames: 1,
                replacementPolicy: ReplacementPolicy.FIFO,
                fetchPolicy: FetchPolicy.DemandPaging,
                placementPolicy: PlacementPolicy.FirstFit
            );

            // Agregamos dos procesos con una página cada uno
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

            // Cargamos la página del primer proceso
            _memoryService.RequestPage(process1.Id, page1.Id);

            // Act
            // Solicitamos la página del segundo proceso, lo que debería provocar un fallo de página y activar la política de reemplazo
            _memoryService.RequestPage(process2.Id, page2.Id);

            // Assert
            // La página del segundo proceso debería estar en memoria
            page2.IsValid.Should().BeTrue();
            page2.FrameNumber.Should().NotBeNull();

            // La página del primer proceso debería haber sido desplazada al espacio de intercambio
            page1.IsValid.Should().BeFalse();
            page1.FrameNumber.Should().BeNull();

            var swapSpace = _memoryService.GetSwapSpace();
            swapSpace.Pages.Should().Contain(page1);
        }

        [Test]
        public void GetPhysicalMemory_Should_Return_Correct_Frame_States()
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

            // Assert
            var physicalMemory = _memoryService.GetPhysicalMemory();
            physicalMemory.Frames.Should().Contain(f => f.IsOccupied && f.ProcessId == process.Id && f.PageId == page.Id);
        }

        [Test]
        public void GetSwapSpace_Should_Return_Pages_In_Swap()
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
            // La página del segundo proceso debería estar en el espacio de intercambio
            swapSpace.Pages.Should().Contain(page2);
            swapSpace.Pages.Should().NotContain(page1);
        }

        [Test]
        public void ConfigureSimulator_Should_Reset_State()
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
                replacementPolicy: ReplacementPolicy.FIFO,
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
        public void RequestPage_Should_Throw_Exception_When_Page_Not_Found()
        {
            // Arrange
            var processId = 1;
            var pageId = 999; // Página que no existe

            // Act
            Action action = () => _memoryService.RequestPage(processId, pageId);

            // Assert
            action.Should().Throw<Exception>().WithMessage("La página solicitada no se encuentra en el espacio de intercambio.");
        }
    }
}
