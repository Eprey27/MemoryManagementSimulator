// src/app/components/memory-view/memory-view.component.ts
import { Component, OnInit } from '@angular/core';
import { MemoryService, PhysicalMemory, Frame } from '../../services/memory.service';

@Component({
  selector: 'app-memory-view',
  templateUrl: './memory-view.component.html',
  styleUrls: ['./memory-view.component.css']
})
export class MemoryViewComponent implements OnInit {
  frames: Frame[] = [];

  constructor(private memoryService: MemoryService) {}

  ngOnInit() {
    this.loadMemory();
  }

  loadMemory() {
    this.memoryService.getMemoryState().subscribe(memory => {
      this.frames = memory.frames;
    });
  }
}
