import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Component } from '@angular/core';

import { email, required } from '@angular/forms/signals';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from "@angular/router";
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-tela-login',
  imports: [ReactiveFormsModule, CommonModule, RouterLink, ],
  templateUrl: './tela-login.html',
  styleUrl: './tela-login.css',
})
export class TelaLogin {
  form: FormGroup;
  

  constructor(private fb: FormBuilder, private authService: AuthService, private router: Router) {
    this.form = this.fb.group({
     
      Email: ['',[Validators.required, Validators.email]],
      Password: ['',[Validators.required,Validators.minLength(3)]],
      });
    
  }

  login() {
    
      if (this.form.valid) {
      this.authService.VerificarLogin(this.form.value).subscribe({
        next : () => {
          this.router.navigate(['/dashboard'])
        },error: (err) => {
          console.log("Erro ao registrar", err)
        }
      })
    
      
    }
  }
 
}


