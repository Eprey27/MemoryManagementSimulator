// src/app/core/services/simulator.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { SimulatorConfig } from '../../shared/models/simulator-config.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SimulatorService {
  private apiUrl = `${environment.apiUrl}/simulator`;

  constructor(private http: HttpClient) {}

  configureSimulator(config: SimulatorConfig): Observable<any> {

  config.fetchPolicy = "0";
  config.placementPolicy = "0";
  config.replacementPolicy = "0";
  config.totalFrames = 0;

    return this.http.post(`${this.apiUrl}/configure`, config);
  }

  requestPage(processId: number, pageId: number): Observable<any> {
    const params = { processId, pageId };
    return this.http.post(`${this.apiUrl}/requestPage`, params);
  }

  getMemoryState(): Observable<any> {
    return this.http.get(`${this.apiUrl}/memory`);
  }

  getSwapSpace(): Observable<any> {
    return this.http.get(`${this.apiUrl}/swap`);
  }

  // Otros métodos según tus necesidades
}
