// src/app/features/simulator/components/add-process/add-process.component.ts

import { Component } from '@angular/core';
import { ProcessService } from './../../../../core/services/process.service';

import { Process } from './../../../../shared/models/process.model';
import { Segment } from './../../../../shared/models/segment.model';

@Component({
  selector: 'app-add-process',
  templateUrl: './add-process.component.html',
  styleUrls: ['./add-process.component.css'],
})
export class AddProcessComponent {
  process: Process = {
    id: 0,
    name: '',
    segments: [],
  };

  constructor(private processService: ProcessService) {}

  /**
   * Agrega el proceso actual al simulador.
   */
  addProcess() {
    let segmentId = 1;
    let pageId = 1;

    this.process.segments.forEach((segment) => {
      segment.id = segmentId++;
      segment.pages.forEach((page) => {
        page.id = pageId++;
        page.processId = this.process.id;
      });
    });

    this.processService.addProcess(this.process).subscribe(
      () => {
        alert('Proceso agregado exitosamente');
        this.process = { id: 0, name: '', segments: [] };
      },
      (error) => {
        console.error('Error al agregar el proceso', error);
      }
    );
  }

  /**
   * Agrega un nuevo segmento al proceso.
   */
  addSegment() {
    this.process.segments.push({
      id: 0,
      size: 1,
      pages: [],
    });
  }

  /**
   * Elimina un segmento del proceso.
   * @param index Índice del segmento a eliminar.
   */
  removeSegment(index: number) {
    this.process.segments.splice(index, 1);
  }

  /**
   * Agrega una nueva página al segmento especificado.
   * @param segment Segmento al que se agregará la página.
   */
  addPage(segment: Segment) {
    segment.pages.push({
      id: 0,
      pageNumber: segment.pages.length,
      isValid: false,
      processId: this.process.id,
      lastAccessTime: '',
      loadTime: '',
    });
  }

  /**
   * Elimina una página del segmento especificado.
   * @param segment Segmento del que se eliminará la página.
   * @param index Índice de la página a eliminar.
   */
  removePage(segment: Segment, index: number) {
    segment.pages.splice(index, 1);
  }
}
