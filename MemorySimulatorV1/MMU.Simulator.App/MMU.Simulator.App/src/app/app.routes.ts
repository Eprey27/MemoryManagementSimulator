import { Routes } from '@angular/router';
import { AddProcessComponent } from '../components/add-process/add-process.component';
import { MemoryViewComponent } from '../components/memory-view/memory-view.component';

export const routes: Routes = [
  { path: 'add-process', component: AddProcessComponent },
  { path: 'memory-view', component: MemoryViewComponent },
  { path: '', redirectTo: '/add-process', pathMatch: 'full' }, // Redirección por defecto
];
