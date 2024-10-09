# Define the project root directory
$rootDir = "MMU.Simulator.App"

# Define the directories and files to be created
$structure = @(
    "e2e",
    "node_modules",
    "src/app/core/core.module.ts",
    "src/app/core/services/process.service.ts",
    "src/app/core/services/memory.service.ts",
    "src/app/core/services/simulator.service.ts",
    "src/app/core/services/swap.service.ts",
    "src/app/shared/shared.module.ts",
    "src/app/shared/models/page.model.ts",
    "src/app/shared/models/segment.model.ts",
    "src/app/shared/models/process.model.ts",
    "src/app/shared/models/frame.model.ts",
    "src/app/shared/models/physical-memory.model.ts",
    "src/app/shared/models/swap-space.model.ts",
    "src/app/shared/models/simulator-config.model.ts",
    "src/app/features/simulator/simulator.module.ts",
    "src/app/features/simulator/components/add-process/add-process.component.ts",
    "src/app/features/simulator/components/add-process/add-process.component.html",
    "src/app/features/simulator/components/add-process/add-process.component.css",
    "src/app/features/simulator/components/memory-view/memory-view.component.ts",
    "src/app/features/simulator/components/memory-view/memory-view.component.html",
    "src/app/features/simulator/components/memory-view/memory-view.component.css",
    "src/app/features/simulator/components/swap-space/swap-space.component.ts",
    "src/app/features/simulator/components/swap-space/swap-space.component.html",
    "src/app/features/simulator/components/swap-space/swap-space.component.css",
    "src/app/features/simulator/components/simulator-config/simulator-config.component.ts",
    "src/app/features/simulator/components/simulator-config/simulator-config.component.html",
    "src/app/features/simulator/components/simulator-config/simulator-config.component.css",
    "src/app/features/simulator/components/simulator-control/simulator-control.component.ts",
    "src/app/features/simulator/components/simulator-control/simulator-control.component.html",
    "src/app/features/simulator/components/simulator-control/simulator-control.component.css",
    "src/app/app.module.ts",
    "src/app/app.component.ts",
    "src/app/app.component.html",
    "src/app/app.component.css",
    "src/app/app-routing.module.ts",
    "src/assets",
    "src/environments/environment.prod.ts",
    "src/environments/environment.ts",
    "src/index.html",
    "src/main.ts",
    "src/polyfills.ts",
    "src/styles.css",
    "src/test.ts",
    "angular.json",
    "package.json",
    "tsconfig.json",
    "tsconfig.app.json",
    "tsconfig.spec.json",
    "proxy.conf.json"
)

# Create directories and files
foreach ($item in $structure) {
    $fullPath = Join-Path $rootDir $item
    $directory = Split-Path $fullPath -Parent

    # Create the directory if it doesn't exist
    if (-not (Test-Path $directory)) {
        New-Item -Path $directory -ItemType Directory -Force
    }

    # Create an empty file if it's not a directory
    if (-not (Test-Path $fullPath)) {
        if ($item -notmatch "/$") {
            New-Item -Path $fullPath -ItemType File -Force
        }
    }
}

Write-Host "Project structure created successfully!"
