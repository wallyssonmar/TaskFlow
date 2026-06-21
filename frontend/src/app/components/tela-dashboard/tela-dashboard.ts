import { CommonModule, isPlatformBrowser } from '@angular/common';
import { ChangeDetectorRef, Component, inject, PLATFORM_ID } from '@angular/core';
import { Projeto } from '../../models/projeto';
import { ProjetoService } from '../../services/projeto-service';
import { RouterLink } from '@angular/router';
import { Router } from '@angular/router';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
  ɵInternalFormsSharedModule,
} from '@angular/forms';
import { filter, Observable, startWith, Subject, switchMap } from 'rxjs';
import { TarefasResponse } from '../../models/tarefas-response';
import { UserService } from '../../services/user-service';
import { User } from '../../models/user';
import { MatSnackBar } from '@angular/material/snack-bar';
import { TarefaService } from '../../services/tarefa-service';

@Component({
  selector: 'app-tela-dashboard',
  imports: [CommonModule, RouterLink, ɵInternalFormsSharedModule, ReactiveFormsModule],
  templateUrl: './tela-dashboard.html',
  styleUrl: './tela-dashboard.css',
})
export class TelaDashboard {
  form: FormGroup;
  formAdd: FormGroup;
  private platformId = inject(PLATFORM_ID);

  private refresh$ = new Subject<void>();

  projetos$ = this.refresh$.pipe(
    startWith(null),
    filter(() => isPlatformBrowser(this.platformId) && !!localStorage.getItem('token')),
    switchMap(() => this.projetoService.getProjetos()),
  );
  isOpen = false;
  mostrarJanelaAdicionar = false;
  mostrarConfirmacao = false;
  mostrarJanelaEditar = false;
  projetoSelecionado: any = null;

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
    private tarefaService: TarefaService,

    private snackBar: MatSnackBar,
    private fb: FormBuilder,
    private router: Router,
  ) {
    this.form = this.fb.group({
      name: ['', [Validators.required, Validators.maxLength(19)]],
      description: ['', [Validators.required, Validators.maxLength(65)]],
      color: ['', Validators.required],
    });

    this.formAdd = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
    });
  }
  ngOnInit() {}

  adicionarMembro(idProjeto: number) {
    const email = this.formAdd.value.email;

    this.projetoService.adicionarMembro(email, idProjeto).subscribe({
      next: () => {
        this.mostrarJanelaAdicionar = false;
        this.snackBar.open('Membro adicionado com sucesso', 'Fechar', {
          duration: 3000,
          panelClass: 'sucess-snackbar',
          horizontalPosition: 'center',
          verticalPosition: 'top',
        });
      },
      error: () => {
        this.snackBar.open('Membro não foi encontrado', 'Fechar', {
          duration: 3000,
          panelClass: 'erro-snackbar',
          horizontalPosition: 'center',
          verticalPosition: 'top',
        });
      },
    });
  }

  abriJanelaAdd(projeto: Projeto) {
    this.projetoSelecionado = projeto;
    this.mostrarJanelaAdicionar = true;
  }
  abrirJanelaEditar(projeto: Projeto) {
    this.projetoSelecionado = projeto;

    this.form.patchValue(projeto);

    this.mostrarJanelaEditar = true;
  }
  cancelarJanelaEditar() {
    this.mostrarJanelaEditar = false;
  }

  abrirConfirmacao(projeto: Projeto) {
    this.mostrarConfirmacao = true;
    this.projetoSelecionado = projeto;
  }

  cancelarConfirmacao() {
    this.mostrarConfirmacao = false;
    this.projetoSelecionado = null;
  }
  criarProjeto() {
    const projeto: Projeto = this.form.value;
    if (projeto) {
      this.projetoService.setProjeto(projeto).subscribe({
        next: () => {
          this.snackBar.open('Projeto com sucesso', 'Fechar', {
            duration: 3000,
            panelClass: 'sucess-snackbar',
            horizontalPosition: 'center',
            verticalPosition: 'top',
          });
          this.refresh$.next();
        },
        error: () => {
          this.snackBar.open('Já existe um projeto com esse nome', 'Fechar', {
            duration: 3000,
            panelClass: 'erro-snackbar',
            horizontalPosition: 'center',
            verticalPosition: 'top',
          });
        },
      });
    }

    this.isOpen = false;
  }

  ProjetoEscolhido(projeto: Projeto) {
    this.router.navigate(['/projeto', projeto.id]);
  }

  excluirProjeto(projeto: Projeto) {
    if (projeto) {
      this.projetoService.deleteProjeto(projeto.id).subscribe({
        next: () => {
          this.refresh$.next();
          this.mostrarConfirmacao = false;
        },
        error: (err) => {
          console.log(err);
        },
      });
    }
  }

  editarProjeto(id: number) {
    const projeto: Projeto = this.form.value;
    this.mostrarJanelaEditar = false;
    console.log(id, projeto);
    if (projeto) {
      this.projetoService.editarProjeto(id, projeto).subscribe({
        next: () => {
          this.snackBar.open('Projeto atualizado com sucesso', 'Fechar', {
            duration: 3000,
            panelClass: 'sucess-snackbar',
            horizontalPosition: 'center',
            verticalPosition: 'top',
          });
          this.refresh$.next();
        },
        error: () => {
          this.snackBar.open('Já existe um projeto com esse nome', 'Fechar', {
            duration: 3000,
            panelClass: 'sucess-snackbar',
            horizontalPosition: 'center',
            verticalPosition: 'top',
          });
        },
      });
    }
  }
}
