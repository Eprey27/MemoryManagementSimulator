# Define the root path for your project
$backendRootPath = "D:\Repos\unir-gii\sistemas-operativos-1\Temas\Temas-7-8\MemorySimulator\Api"
$frontendRootPath = "D:\Repos\unir-gii\sistemas-operativos-1\Temas\Temas-7-8\MemorySimulator\Client"

# Create project folder structure for Backend
New-Item -Path $backendRootPath -ItemType Directory

$backendFolders = @(
    "Controllers",
    "Models",
    "Services"
)

foreach ($folder in $backendFolders) {
    New-Item -Path "$backendRootPath\$folder" -ItemType Directory
}

# Create files in Controllers folder
$controllers = @(
    "ProcessesController.cs",
    "SimulatorController.cs"
)

foreach ($file in $controllers) {
    New-Item -Path "$backendRootPath\Controllers\$file" -ItemType File
}

# Create files in Models folder
$models = @(
    "Process.cs",
    "Segment.cs",
    "Page.cs",
    "Frame.cs",
    "PhysicalMemory.cs",
    "SwapSpace.cs",
    "ReplacementPolicy.cs",
    "FetchPolicy.cs",
    "PlacementPolicy.cs"
)

foreach ($file in $models) {
    New-Item -Path "$backendRootPath\Models\$file" -ItemType File
}

# Create files in Services folder
$services = @(
    "MemoryManagementService.cs"
)

foreach ($file in $services) {
    New-Item -Path "$backendRootPath\Services\$file" -ItemType File
}

# Create remaining files in the root directory
$backendRootFiles = @(
    "Program.cs",
    "MemorySimulatorBackend.csproj"
)

foreach ($file in $backendRootFiles) {
    New-Item -Path "$backendRootPath\$file" -ItemType File
}

Write-Host "Backend project structure created successfully at $backendRootPath."

# Create project folder structure for Frontend
New-Item -Path $frontendRootPath -ItemType Directory

$frontendFolders = @(
    "src",
    "src\app",
    "src\app\components",
    "src\app\components\add-process",
    "src\app\components\memory-view",
    "src\app\components\swap-space",
    "src\app\components\simulator-config",
    "src\app\components\simulator-control",
    "src\app\services"
)

foreach ($folder in $frontendFolders) {
    New-Item -Path "$frontendRootPath\$folder" -ItemType Directory
}

# Create files in components/add-process folder
$addProcessFiles = @(
    "add-process.component.ts",
    "add-process.component.html",
    "add-process.component.css"
)

foreach ($file in $addProcessFiles) {
    New-Item -Path "$frontendRootPath\src\app\components\add-process\$file" -ItemType File
}

# Create files in components/memory-view folder
$memoryViewFiles = @(
    "memory-view.component.ts",
    "memory-view.component.html",
    "memory-view.component.css"
)

foreach ($file in $memoryViewFiles) {
    New-Item -Path "$frontendRootPath\src\app\components\memory-view\$file" -ItemType File
}

# Create files in components/swap-space folder
$swapSpaceFiles = @(
    "swap-space.component.ts",
    "swap-space.component.html",
    "swap-space.component.css"
)

foreach ($file in $swapSpaceFiles) {
    New-Item -Path "$frontendRootPath\src\app\components\swap-space\$file" -ItemType File
}

# Create files in components/simulator-config folder
$simulatorConfigFiles = @(
    "simulator-config.component.ts",
    "simulator-config.component.html",
    "simulator-config.component.css"
)

foreach ($file in $simulatorConfigFiles) {
    New-Item -Path "$frontendRootPath\src\app\components\simulator-config\$file" -ItemType File
}

# Create files in components/simulator-control folder
$simulatorControlFiles = @(
    "simulator-control.component.ts",
    "simulator-control.component.html",
    "simulator-control.component.css"
)

foreach ($file in $simulatorControlFiles) {
    New-Item -Path "$frontendRootPath\src\app\components\simulator-control\$file" -ItemType File
}

# Create files in services folder
$servicesFiles = @(
    "process.service.ts",
    "memory.service.ts",
    "simulator.service.ts",
    "swap.service.ts"
)

foreach ($file in $servicesFiles) {
    New-Item -Path "$frontendRootPath\src\app\services\$file" -ItemType File
}

# Create remaining files in the src/app folder
$appFiles = @(
    "app.module.ts",
    "app.component.ts",
    "app.component.html"
)

foreach ($file in $appFiles) {
    New-Item -Path "$frontendRootPath\src\app\$file" -ItemType File
}

# Create proxy.conf.json in the src folder
New-Item -Path "$frontendRootPath\src\proxy.conf.json" -ItemType File

# Create package.json in the root directory
New-Item -Path "$frontendRootPath\package.json" -ItemType File

Write-Host "Frontend project structure created successfully at $frontendRootPath."