// src/app/components/simulator-config/simulator-config.component.ts
import { Component } from '@angular/core';
import { SimulatorService, SimulatorConfig } from '../../services/simulator.service';

@Component({
  selector: 'app-simulator-config',
  templateUrl: './simulator-config.component.html',
  styleUrls: ['./simulator-config.component.css']
})
export class SimulatorConfigComponent {
  config: SimulatorConfig = {
    totalFrames: 100,
    replacementPolicy: 'LRU',
    fetchPolicy: 'DemandPaging',
    placementPolicy: 'FirstFit'
  };

  policies = {
    replacementPolicies: ['FIFO', 'LRU', 'Optimal'],
    fetchPolicies: ['DemandPaging', 'Prepaging'],
    placementPolicies: ['FirstFit', 'BestFit', 'WorstFit']
  };

  constructor(private simulatorService: SimulatorService) {}

  configureSimulator() {
    this.simulatorService.configureSimulator(this.config).subscribe(response => {
      alert('Configuración aplicada exitosamente');
    });
  }
}
