// src/app/shared/models/frame.model.ts
export interface Frame {
    frameNumber: number;
    isOccupied: boolean;
    processId?: number;
    pageId?: number;
  }
  