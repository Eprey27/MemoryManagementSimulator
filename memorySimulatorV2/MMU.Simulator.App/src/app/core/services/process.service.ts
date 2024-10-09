// src/app/core/services/process.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Process } from '../../shared/models/process.model';

@Injectable({
  providedIn: 'root',
})
export class ProcessService {
  private apiUrl = '/api/processes';

  constructor(private http: HttpClient) {}

  addProcess(process: Process): Observable<any> {
    return this.http.post(this.apiUrl, process);
  }

  getProcesses(): Observable<Process[]> {
    return this.http.get<Process[]>(this.apiUrl);
  }
}
