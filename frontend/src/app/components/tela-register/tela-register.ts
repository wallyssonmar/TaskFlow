import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Component } from '@angular/core';

import { email, required } from '@angular/forms/signals';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth-service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-tela-register',
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './tela-register.html',
  styleUrl: './tela-register.css',
})
export class TelaRegister {
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router,
    private snackBar: MatSnackBar,
  ) {
    this.form = this.fb.group(
      {
        Name: ['', Validators.required],
        Email: ['', [Validators.required, Validators.email]],
        Password: ['', [Validators.required, Validators.minLength(3)]],
        ConfirmPassword: ['', [Validators.required, Validators.minLength(3)]],
      },
      {
        validators: senhaIgualValidator,
      },
    );
  }

  registrar() {
    if (this.form.valid) {
      this.authService.setRegisterUser(this.form.value).subscribe({
        next: () => {
          this.snackBar.open('Conta criada com sucesso', 'Fechar', {
            duration: 3000,
            panelClass: 'sucess-snackbar',
            horizontalPosition: 'center',
            verticalPosition: 'top',
          });
          this.router.navigate(['/login']);
        },
        error: () => {
          this.snackBar.open('Já existe um usuario com esse email.', 'Fechar', {
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
export function senhaIgualValidator(control: AbstractControl) {
  const senha = control.get('Password')?.value;
  const confirmar = control.get('ConfirmPassword')?.value;

  if (senha !== confirmar) {
    return { senhaDiferente: true };
  }

  return null;
}
