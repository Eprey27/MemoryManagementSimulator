// src/app/features/simulator/simulator.module.ts
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AddProcessComponent } from './components/add-process/add-process.component';
import { MemoryViewComponent } from './components/memory-view/memory-view.component';
import { SwapSpaceComponent } from './components/swap-space/swap-space.component';
import { SimulatorConfigComponent } from './components/simulator-config/simulator-config.component';
import { SimulatorControlComponent } from './components/simulator-control/simulator-control.component';
import { FormsModule } from '@angular/forms';
import { SharedModule } from '../../shared/shared.module';

import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';

@NgModule({
  declarations: [
    AddProcessComponent,
    MemoryViewComponent,
    SwapSpaceComponent,
    SimulatorConfigComponent,
    SimulatorControlComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
  ],
  exports: [
    AddProcessComponent,
    MemoryViewComponent,
    SwapSpaceComponent,
    SimulatorConfigComponent,
    SimulatorControlComponent,
  ],
})
export class SimulatorModule {}
