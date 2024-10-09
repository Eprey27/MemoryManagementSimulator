import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SwapService } from '../services/swap.service';
import { HttpClientModule } from '@angular/common/http';

@NgModule({
  declarations: [
  ],
  imports: [
    CommonModule, // Permite usar directivas comunes como *ngIf y *ngFor
    HttpClientModule // Importa el módulo HttpClient para el servicio SwapService
  ],
  providers: [
    SwapService // Provee el servicio para manejar el espacio de intercambio (swap-space)
  ],
  exports: [
     // Exporta el componente para que pueda ser utilizado en otros módulos
  ]
})
export class SwapSpaceModule { }
