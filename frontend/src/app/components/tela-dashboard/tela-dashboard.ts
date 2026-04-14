import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Projeto } from '../../models/projeto';
import { ProjetoService } from '../../services/projeto-service';
import { RouterLink } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, ɵInternalFormsSharedModule } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-tela-dashboard',
  imports: [CommonModule, RouterLink, ɵInternalFormsSharedModule,ReactiveFormsModule],
  templateUrl: './tela-dashboard.html',
  styleUrl: './tela-dashboard.css',
})
export class TelaDashboard {
  form: FormGroup;
  
  projetos$!: Observable<Projeto[]>;
  isOpen = false;

  colors = ['#3B82F6', '#8B5CF6', '#22C55E', '#EF4444', '#EAB308', '#EC4899', '#6366F1', '#14B8A6'];

  selectedColor?: string;

  constructor(
    private projetoService: ProjetoService,
    private fb: FormBuilder,
  ) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      descricao: ['', [Validators.required, Validators.maxLength(65)]],
      selectedColor: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.projetos$ = this.projetoService.getProjetos();
  }

  
  getProjetos(){
   this.projetos$ = this.projetoService.getProjetos();
  }


  criarProjeto() {
    if (this.form.valid) {
      
      this.projetoService.setProjeto(this.form.value);
      this.getProjetos();
      
    }
  }

  tarefaEscolhida(projeto: Projeto){
    this.projetoService.setTarefa(projeto);
  }
}
