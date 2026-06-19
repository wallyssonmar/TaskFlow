import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Component } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-tela-login',
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './tela-login.html',
  styleUrl: './tela-login.css',
})
export class TelaLogin {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar,
  ) {
    this.form = this.fb.group({
      Email: ['', [Validators.required, Validators.email]],
      Password: ['', [Validators.required, Validators.minLength(3)]],
    });
  }

  login() {
    if (this.form.valid) {
      this.authService.VerificarLogin(this.form.value).subscribe({
        next: (response) => {
          this.snackBar.open('Login com sucesso.', 'Fechar', {
            duration: 3000,
            panelClass: 'sucess-snackbar',
            horizontalPosition: 'center',
            verticalPosition: 'top',
          });
          this.router.navigate(['/dashboard']);
        },
        error: (err) => {
          this.snackBar.open('Email ou senha incorretos.', 'Fechar', {
            duration: 3000,
            panelClass: 'erro-snackbar',
            horizontalPosition: 'center',
            verticalPosition: 'top',
          });
        },
      });
    }
  }
}
