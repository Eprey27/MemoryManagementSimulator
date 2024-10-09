// src/app/features/simulator/components/memory-view/memory-view.component.ts

import { Component, OnInit } from '@angular/core';
import { MemoryService } from './../../../../core/services/memory.service';
import { Frame } from './../../../../shared/models/frame.model';

@Component({
  selector: 'app-memory-view',
  templateUrl: './memory-view.component.html',
  styleUrls: ['./memory-view.component.css'],
})
export class MemoryViewComponent implements OnInit {
  frames: Frame[] = [];

  constructor(private memoryService: MemoryService) {}

  ngOnInit() {
    this.loadMemory();
  }

  /**
   * Carga el estado actual de la memoria física desde el servicio.
   */
  loadMemory() {
    this.memoryService.getMemoryState().subscribe(
      (memory) => {
        this.frames = memory.frames;
      },
      (error) => {
        console.error('Error al cargar la memoria física', error);
      }
    );
  }
}
