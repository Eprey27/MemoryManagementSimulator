// src/app/app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AddProcessComponent } from './features/simulator/components/add-process/add-process.component';
import { MemoryViewComponent } from './features/simulator/components/memory-view/memory-view.component';
import { SwapSpaceComponent } from './features/simulator/components/swap-space/swap-space.component';
import { SimulatorConfigComponent } from './features/simulator/components/simulator-config/simulator-config.component';
import { SimulatorControlComponent } from './features/simulator/components/simulator-control/simulator-control.component';

const routes: Routes = [
  { path: '', redirectTo: '/simulator', pathMatch: 'full' },
  {
    path: 'simulator',
    children: [
      { path: '', component: SimulatorConfigComponent },
      { path: 'add-process', component: AddProcessComponent },
      { path: 'memory-view', component: MemoryViewComponent },
      { path: 'swap-space', component: SwapSpaceComponent },
      { path: 'control', component: SimulatorControlComponent },
    ],
  },
  { path: '**', redirectTo: '/simulator' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
