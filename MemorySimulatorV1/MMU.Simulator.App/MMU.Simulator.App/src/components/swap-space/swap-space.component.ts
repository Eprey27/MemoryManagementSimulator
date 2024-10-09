// src/app/components/swap-space/swap-space.component.ts
import { Component, OnInit } from '@angular/core';
import { SwapService, SwapSpace } from '../../services/swap.service';
import { Page } from '../../services/process.service';

@Component({
  selector: 'app-swap-space',
  templateUrl: './swap-space.component.html',
  styleUrls: ['./swap-space.component.css']
})
export class SwapSpaceComponent implements OnInit {
  pages: Page[] = [];

  constructor(private swapService: SwapService) {}

  ngOnInit() {
    this.loadSwapSpace();
  }

  loadSwapSpace() {
    this.swapService.getSwapSpace().subscribe(swapSpace => {
      this.pages = swapSpace.pages;
    });
  }
}
