// src/app/services/process.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Page {
  id: number;
  pageNumber: number;
  isValid: boolean;
  frameNumber?: number;
  lastAccessTime: string;
  loadTime: string;
  processId: number;
}

export interface Segment {
  id: number;
  size: number;
  pages: Page[];
}

export interface Process {
  id: number;
  name: string;
  segments: Segment[];
}

@Injectable({
  providedIn: 'root'
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
