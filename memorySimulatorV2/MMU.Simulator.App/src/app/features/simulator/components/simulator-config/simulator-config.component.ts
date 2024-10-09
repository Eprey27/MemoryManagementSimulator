// src/app/features/simulator/components/simulator-config/simulator-config.component.ts

import { Component } from '@angular/core';
import { SimulatorService } from '../../../core/services/simulator.service';
import { SimulatorConfig } from '../../../shared/models/simulator-config.model';

@Component({
  selector: 'app-simulator-config',
  templateUrl: './simulator-config.component.html',
  styleUrls: ['./simulator-config.component.css'],
})
export class SimulatorConfigComponent {
  config: SimulatorConfig = {
    totalFrames: 100,
    replacementPolicy: 'LRU',
    fetchPolicy: 'DemandPaging',
    placementPolicy: 'FirstFit',
  };

  policies = {
    replacementPolicies: ['FIFO', 'LRU', 'Optimal'],
    fetchPolicies: ['DemandPaging', 'Prepaging'],
    placementPolicies: ['FirstFit', 'BestFit', 'WorstFit'],
  };

  constructor(private simulatorService: SimulatorService) {}

  /**
   * Envía la configuración actual del simulador al backend.
   */
  configureSimulator() {
    this.simulatorService.configureSimulator(this.config).subscribe(
      () => {
        alert('Configuración aplicada exitosamente');
      },
      (error) => {
        console.error('Error al configurar el simulador', error);
      }
    );
  }
}
