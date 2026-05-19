import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Component } from '@angular/core';

import { email, required } from '@angular/forms/signals';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { RegisterService } from '../../services/register-service';

@Component({
  selector: 'app-tela-register',
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './tela-register.html',
  styleUrl: './tela-register.css',
})
export class TelaRegister {
  form: FormGroup;

  constructor(private fb: FormBuilder, private registerService: RegisterService,private router: Router) {
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
      this.registerService.setRegisterUser(this.form.value).subscribe({
        next : () => {
          this.router.navigate(['/login'])
        },error: (err) => {
          console.log("Erro ao registrar", err)
        }
      })
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
