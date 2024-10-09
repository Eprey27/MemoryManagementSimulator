// src/app/core/services/memory.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { PhysicalMemory } from '../../shared/models/physical-memory.model';

@Injectable({
  providedIn: 'root',
})
export class MemoryService {
  private apiUrl = '/api/simulator/memory';

  constructor(private http: HttpClient) {}

  getMemoryState(): Observable<PhysicalMemory> {
    return this.http.get<PhysicalMemory>(this.apiUrl);
  }
}
