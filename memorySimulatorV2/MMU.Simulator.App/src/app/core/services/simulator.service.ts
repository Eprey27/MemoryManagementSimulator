// src/app/core/services/simulator.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { SimulatorConfig } from '../../shared/models/simulator-config.model';

@Injectable({
  providedIn: 'root',
})
export class SimulatorService {
  private apiUrl = '/api/simulator';

  constructor(private http: HttpClient) {}

  configureSimulator(config: SimulatorConfig): Observable<any> {
    return this.http.post(`${this.apiUrl}/configure`, config);
  }

  requestPage(processId: number, pageId: number): Observable<any> {
    return this.http.post(`${this.apiUrl}/requestPage`, { processId, pageId });
  }
}
