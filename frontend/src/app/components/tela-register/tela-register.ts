import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { Component } from '@angular/core';

import { email, required } from '@angular/forms/signals';
import { CommonModule } from '@angular/common';
import { RouterLink } from "@angular/router";

@Component({
  selector: 'app-tela-register',
  imports: [ReactiveFormsModule, CommonModule, RouterLink],
  templateUrl: './tela-register.html',
  styleUrl: './tela-register.css',
})
export class TelaRegister {
  form: FormGroup;
  

  constructor(private fb: FormBuilder) {
    this.form = this.fb.group({
      nome: ['',Validators.required],
      email: ['',[Validators.required, Validators.email]],
      senha: ['',[Validators.required,Validators.minLength(3)]],
      confirmarSenha: ['',[Validators.required,Validators.minLength(3)]]
    },{
      validators: senhaIgualValidator
    });
    
  }

  registrar() {
    if (this.form.valid) {
      console.log(this.form.value);
      console.log("passou aqui");
      
    }
  }
 
}
export function senhaIgualValidator(control: AbstractControl) {
    const senha = control.get('senha')?.value;
    const confirmar = control.get('confirmarSenha')?.value;
    
  
    if(senha !== confirmar){
      
      return {senhaDiferente : true};
    }

    return null;
  }