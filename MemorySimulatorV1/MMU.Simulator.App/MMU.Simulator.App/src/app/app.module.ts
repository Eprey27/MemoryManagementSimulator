import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router'; // <-- Importa el módulo de rutas

// Angular Material Modules
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatSelectModule } from '@angular/material/select';
import { MatToolbarModule } from '@angular/material/toolbar';

// Componentes
import { AppComponent } from './app.component';
import { AddProcessComponent } from '../components/add-process/add-process.component';
import { MemoryViewComponent } from '../components/memory-view/memory-view.component';
import { SwapSpaceComponent } from '../components/swap-space/swap-space.component';
import { SimulatorConfigComponent } from '../components/simulator-config/simulator-config.component';
import { SimulatorControlComponent } from '../components/simulator-control/simulator-control.component';

// Importa tus rutas desde el archivo `app.routes.ts`
import { routes } from './app.routes';

@NgModule({
  declarations: [
    AppComponent,
    AddProcessComponent,
    MemoryViewComponent,
    SwapSpaceComponent,
    SimulatorConfigComponent,
    SimulatorControlComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    BrowserAnimationsModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatSelectModule,
    MatToolbarModule,
    RouterModule.forRoot(routes) // <-- Configura las rutas en tu módulo principal
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
