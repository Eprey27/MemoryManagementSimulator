// src/app/components/simulator-control/simulator-control.component.ts
import { Component } from '@angular/core';
import { SimulatorService } from '../../services/simulator.service';

@Component({
  selector: 'app-simulator-control',
  templateUrl: './simulator-control.component.html',
  styleUrls: ['./simulator-control.component.css']
})
export class SimulatorControlComponent {
  constructor(private simulatorService: SimulatorService) {}

  requestPage() {
    const processId = Number(prompt('Ingrese el ID del proceso:'));
    const pageId = Number(prompt('Ingrese el ID de la página:'));

    if (!isNaN(processId) && !isNaN(pageId)) {
      this.simulatorService.requestPage(processId, pageId).subscribe(response => {
        alert('Página solicitada correctamente');
      });
    }
  }
}
