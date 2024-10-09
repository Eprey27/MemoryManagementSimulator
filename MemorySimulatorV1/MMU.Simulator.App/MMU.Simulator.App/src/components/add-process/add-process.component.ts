// src/app/components/add-process/add-process.component.ts
import { Component } from '@angular/core';
import { ProcessService, Process, Segment, Page } from '../../services/process.service';

@Component({
  selector: 'app-add-process',
  templateUrl: './add-process.component.html',
  styleUrls: ['./add-process.component.css']
})
export class AddProcessComponent {
  process: Process = {
    id: 0,
    name: '',
    segments: []
  };

  constructor(private processService: ProcessService) {}

  addProcess() {
    // Generar IDs únicos para segmentos y páginas
    let segmentId = 1;
    let pageId = 1;

    this.process.segments.forEach(segment => {
      segment.id = segmentId++;
      segment.pages.forEach(page => {
        page.id = pageId++;
        page.processId = this.process.id;
      });
    });

    this.processService.addProcess(this.process).subscribe(response => {
      alert('Proceso agregado exitosamente');
      this.process = { id: 0, name: '', segments: [] };
    });
  }

  addSegment() {
    this.process.segments.push({
      id: 0,
      size: 1,
      pages: []
    });
  }

  removeSegment(index: number) {
    this.process.segments.splice(index, 1);
  }

  addPage(segment: Segment) {
    segment.pages.push({
      id: 0,
      pageNumber: segment.pages.length,
      isValid: false,
      processId: this.process.id,
      lastAccessTime: '',
      loadTime: ''
    });
  }

  removePage(segment: Segment, index: number) {
    segment.pages.splice(index, 1);
  }
}
