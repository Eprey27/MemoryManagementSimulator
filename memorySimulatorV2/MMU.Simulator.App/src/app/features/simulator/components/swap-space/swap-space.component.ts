// src/app/features/simulator/components/swap-space/swap-space.component.ts

import { Component, OnInit } from '@angular/core';
import { SwapService } from '../../../core/services/swap.service';
import { Page } from '../../../shared/models/page.model';

@Component({
  selector: 'app-swap-space',
  templateUrl: './swap-space.component.html',
  styleUrls: ['./swap-space.component.css'],
})
export class SwapSpaceComponent implements OnInit {
  pages: Page[] = [];

  constructor(private swapService: SwapService) {}

  ngOnInit() {
    this.loadSwapSpace();
  }

  /**
   * Carga el estado actual del espacio de intercambio desde el servicio.
   */
  loadSwapSpace() {
    this.swapService.getSwapSpace().subscribe(
      (swapSpace) => {
        this.pages = swapSpace.pages;
      },
      (error) => {
        console.error('Error al cargar el espacio de intercambio', error);
      }
    );
  }
}
