// src/app/shared/models/process.model.ts
import { Segment } from './segment.model';

export interface Process {
  id: number;
  name: string;
  segments: Segment[];
}
