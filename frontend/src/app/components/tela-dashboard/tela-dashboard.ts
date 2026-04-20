import { CommonModule } from '@angular/common';
import { ChangeDetectorRef, Component } from '@angular/core';
import { Projeto } from '../../models/projeto';
import { ProjetoService } from '../../services/projeto-service';
import { RouterLink } from '@angular/router';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
  ɵInternalFormsSharedModule,
} from '@angular/forms';
import { Observable, startWith, Subject, switchMap } from 'rxjs';
import { error } from 'node:console';

@Component({
  selector: 'app-tela-dashboard',
  imports: [CommonModule, RouterLink, ɵInternalFormsSharedModule, ReactiveFormsModule],
  templateUrl: './tela-dashboard.html',
  styleUrl: './tela-dashboard.css',
})
export class TelaDashboard {
  form: FormGroup;
  private refresh$ = new Subject<void>();
  projetos$ = this.refresh$.pipe(
    startWith(null),
    switchMap(() => this.projetoService.getProjetos()),
  );
  isOpen = false;
  mostrarConfirmacao = false;
  mostrarJanelaEditar = false;
  projetoSelecionado : any = null;
  
  coloropts = [
    '#3B82F6',
    '#8B5CF6',
    '#22C55E',
    '#EF4444',
    '#EAB308',
    '#EC4899',
    '#6366F1',
    '#14B8A6',
  ];

  color?: string;
  qtdMembros: any;
  qtdTarefa: any;
  qtdTarefaConcluida: any;

  constructor(
    private projetoService: ProjetoService,
    private fb: FormBuilder,
  ) {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(19)]],
      description: ['', [Validators.required, Validators.maxLength(65)]],
      color: ['', Validators.required],
    });
  }
  abrirJanelaEditar(projeto: Projeto){
  this.projetoSelecionado = projeto;

  this.form.patchValue(projeto);

  this.mostrarJanelaEditar = true;
}
  cancelarJanelaEditar(){
    this.mostrarJanelaEditar = false;
    
  }

  abrirConfirmacao(projeto: Projeto){
    this.mostrarConfirmacao = true;
    this.projetoSelecionado = projeto;

  }

  cancelarConfirmacao(){
    this.mostrarConfirmacao = false;
    this.projetoSelecionado = null;
  }
  criarProjeto() {
    const projeto: Projeto = this.form.value;
    if (projeto) {
      this.projetoService.setProjeto(projeto).subscribe({
        next: () => {
          this.refresh$.next();
        },
        error: (err) => {
          console.log(err.error.errors);
        },
      });
    }

    this.isOpen = false;
  }

  tarefaEscolhida(projeto: Projeto) {
    this.projetoService.setTarefa(projeto);
  }

  excluirProjeto(projeto: Projeto) {
    if (projeto) {
      this.projetoService.deleteProjeto(projeto.id).subscribe({
        next: () => {
          this.refresh$.next();
          this.mostrarConfirmacao = false;
        },
        error: (err) => {
          console.log(err.error.errors);
        },
      });
    }
  }

  editarProjeto(id: number){
    
    const projeto: Projeto = this.form.value;
    this.mostrarJanelaEditar = false;
    console.log(id, projeto)
    if(projeto){
      this.projetoService.editarProjeto(id, projeto).subscribe({
        next:() => {
          this.refresh$.next();
        },
        error: (err) => {
          console.log(err.error.errors);
        }
      })
    }
  }
}
