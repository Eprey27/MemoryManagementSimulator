// src/app/services/memory.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Frame {
  frameNumber: number;
  isOccupied: boolean;
  processId?: number;
  pageId?: number;
}

export interface PhysicalMemory {
  totalFrames: number;
  frames: Frame[];
}

@Injectable({
  providedIn: 'root'
})
export class MemoryService {
  private apiUrl = '/api/simulator/memory';

  constructor(private http: HttpClient) {}

  getMemoryState(): Observable<PhysicalMemory> {
    return this.http.get<PhysicalMemory>(this.apiUrl);
  }
}
