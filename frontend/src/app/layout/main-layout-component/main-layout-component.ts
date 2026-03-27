
import { Component,  } from '@angular/core';

import { RouterOutlet } from '@angular/router';
import { Cabecalho } from "../../components/cabecalho/cabecalho";


@Component({
  selector: 'app-main-layout-component',
  imports: [RouterOutlet, Cabecalho],
  templateUrl: './main-layout-component.html',
  styleUrl: './main-layout-component.css',
})
export class MainLayoutComponent {

}
