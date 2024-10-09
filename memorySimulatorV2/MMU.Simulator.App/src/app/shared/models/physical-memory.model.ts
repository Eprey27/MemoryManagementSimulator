// src/app/shared/models/physical-memory.model.ts
import { Frame } from './frame.model';

export interface PhysicalMemory {
  totalFrames: number;
  frames: Frame[];
}
