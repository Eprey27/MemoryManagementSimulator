// src/app/shared/models/page.model.ts
export interface Page {
    id: number;
    pageNumber: number;
    isValid: boolean;
    frameNumber?: number;
    lastAccessTime: string;
    loadTime: string;
    processId: number;
  }
  