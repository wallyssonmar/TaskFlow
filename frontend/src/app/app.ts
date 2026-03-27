import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { TelaRegister } from "./components/tela-register/tela-register";
import { TelaLogin } from "./components/tela-login/tela-login";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, TelaRegister, TelaLogin],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('TaskFlow');
}
