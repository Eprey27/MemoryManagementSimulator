// src/app/shared/models/segment.model.ts
import { Page } from './page.model';

export interface Segment {
  id: number;
  size: number;
  pages: Page[];
}
