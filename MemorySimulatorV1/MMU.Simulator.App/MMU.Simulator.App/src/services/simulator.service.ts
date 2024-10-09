// src/app/services/simulator.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface SimulatorConfig {
  totalFrames: number;
  replacementPolicy: string;
  fetchPolicy: string;
  placementPolicy: string;
}

@Injectable({
  providedIn: 'root'
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
