// src/app/core/services/swap.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { SwapSpace } from '../../shared/models/swap-space.model';

@Injectable({
  providedIn: 'root',
})
export class SwapService {
  private apiUrl = '/api/simulator/swap';

  constructor(private http: HttpClient) {}

  getSwapSpace(): Observable<SwapSpace> {
    return this.http.get<SwapSpace>(this.apiUrl);
  }
}
