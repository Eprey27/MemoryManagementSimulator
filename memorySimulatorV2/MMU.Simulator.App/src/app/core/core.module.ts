// src/app/core/core.module.ts
import { NgModule, Optional, SkipSelf } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { ProcessService } from './services/process.service';
import { MemoryService } from './services/memory.service';
import { SimulatorService } from './services/simulator.service';
import { SwapService } from './services/swap.service';

@NgModule({
  imports: [HttpClientModule],
  providers: [
    ProcessService,
    MemoryService,
    SimulatorService,
    SwapService,
  ],
})
export class CoreModule {
  constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
    if (parentModule) {
      throw new Error(
        'CoreModule ya ha sido cargado. Debe importarse solo en AppModule.'
      );
    }
  }
}
