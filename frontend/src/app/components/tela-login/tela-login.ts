import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Component } from '@angular/core';

import { email, required } from '@angular/forms/signals';
import { CommonModule } from '@angular/common';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-tela-login',
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './tela-login.html',
  styleUrl: './tela-login.css',
})
export class TelaLogin {
  form: FormGroup;
  

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
     
      email: ['',[Validators.required, Validators.email]],
      senha: ['',[Validators.required,Validators.minLength(3)]],
      });
    
  }

  registrar() {
    if (this.form.valid) {
      console.log(this.form.value);
      console.log("passou aqui");
      
    }
  }
 
}


