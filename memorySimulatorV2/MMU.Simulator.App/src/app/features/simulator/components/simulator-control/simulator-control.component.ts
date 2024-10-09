// src/app/features/simulator/components/simulator-control/simulator-control.component.ts

import { Component } from '@angular/core';
import { SimulatorService } from './../../../../core/services/simulator.service';

@Component({
  selector: 'app-simulator-control',
  templateUrl: './simulator-control.component.html',
  styleUrls: ['./simulator-control.component.css'],
})
export class SimulatorControlComponent {
  constructor(private simulatorService: SimulatorService) {}

  /**
   * Solicita una página específica de un proceso al simulador.
   */
  requestPage() {
    const processId = Number(prompt('Ingrese el ID del proceso:'));
    const pageId = Number(prompt('Ingrese el ID de la página:'));

    if (!isNaN(processId) && !isNaN(pageId)) {
      this.simulatorService.requestPage(processId, pageId).subscribe(
        () => {
          alert('Página solicitada correctamente');
        },
        (error) => {
          console.error('Error al solicitar la página', error);
        }
      );
    } else {
      alert('IDs inválidos. Por favor, ingrese números válidos.');
    }
  }
}
