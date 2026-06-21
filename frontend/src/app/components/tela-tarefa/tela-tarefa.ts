import { Component, CUSTOM_ELEMENTS_SCHEMA, inject, NgZone, PLATFORM_ID } from '@angular/core';
import { ProjetoService } from '../../services/projeto-service';
import { Projeto } from '../../models/projeto';
import { ActivatedRoute, RouterLink } from '@angular/router';
import {
  CdkDragDrop,
  DragDropModule,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { CommonModule, isPlatformBrowser } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { TarefaService } from '../../services/tarefa-service';
import { Tarefa } from '../../models/tarefa';
import { filter, map, Observable, startWith, Subject, switchMap, tap } from 'rxjs';
import { TarefasResponse } from '../../models/tarefas-response';
import { error } from 'node:console';
import { MatSnackBar } from '@angular/material/snack-bar';
import { User } from '../../models/user';
import { UserService } from '../../services/user-service';

@Component({
  selector: 'app-tela-tarefa',
  imports: [RouterLink, DragDropModule, CommonModule, ReactiveFormsModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  templateUrl: './tela-tarefa.html',
  styleUrl: './tela-tarefa.css',
})
export class TelaTarefa {
  pessoaLogada$!: Observable<User | null>;
  private platformId = inject(PLATFORM_ID);
  tarefa: Tarefa = {} as Tarefa;
  tarefaSelecionada: Tarefa = {} as Tarefa;
  mostrarConfirmacao = false;
  mostrarJanelaEditar = false;
  idLink: number = 0;
  listaPrioridades = ['Baixa', 'Média', 'Alta'];
  listaStatus = ['A Fazer', 'Em progresso', 'Concluído'];
  status = '';
  prioridade = '';
  openPrioridade = false;
  openStatus = false;
  form: FormGroup;
  pessoaLogada: string = 'Almeida';
  listaAfazers: Tarefa[] = [];
  listaProgressos: Tarefa[] = [];
  listaConcluidas: Tarefa[] = [];
  isOpen = false;
  projetoSelecionado?: Projeto;
  private refresh$ = new Subject<void>();
  projeto$!: Observable<Projeto>;
  tarefas$ = this.refresh$.pipe(
    startWith(void 0),
    filter(() => isPlatformBrowser(this.platformId) && !!localStorage.getItem('token')),
    switchMap(() => this.tarefaService.getTarefas(this.idLink)),
  );

  constructor(
    private projetoService: ProjetoService,
    private userService: UserService,
    private route: ActivatedRoute,
    private tarefaService: TarefaService,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
  ) {
    this.pessoaLogada$ = this.userService.currentUser$;
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: ['', [Validators.required, Validators.maxLength(65)]],
      prioridade: ['', Validators.required],
      status: ['', Validators.required],
    });
  }

  ngOnInit() {
    if (isPlatformBrowser(this.platformId) && localStorage.getItem('token')) {
      this.projeto$ = this.route.params.pipe(
        map((params) => Number(params['id'])),
        tap((id) => {
          this.idLink = id;
        }),
        switchMap((id) => this.projetoService.getProjetoById(id)),
        tap((projeto) => {
          this.projetoSelecionado = projeto;
        }),
      );
    }
  }

  drop(event: CdkDragDrop<Tarefa[]>) {
    if (event.previousContainer === event.container) {
      moveItemInArray(event.container.data, event.previousIndex, event.currentIndex);
    } else {
      transferArrayItem(
        event.previousContainer.data,
        event.container.data,
        event.previousIndex,
        event.currentIndex,
      );
      const novoStatus = event.container.id;
      let status = '';

      switch (novoStatus) {
        case 'listaAfazers':
          status = 'A Fazer';
          break;

        case 'listaProgressos':
          status = 'Em progresso';
          break;

        case 'listaConcluidas':
          status = 'Concluído';
          break;
      }
      const tarefa = event.container.data[event.currentIndex];

      this.tarefaService.atualizarStatusTarefa(tarefa.id, status).subscribe();
    }
  }

  abrirConfirmacao(tarefa: Tarefa) {
    this.mostrarConfirmacao = true;
    this.tarefaSelecionada = tarefa;
  }
  cancelarConfirmacao() {
    this.mostrarConfirmacao = false;
    this.mostrarJanelaEditar = false;
  }
  abrirJanelaEditar(tarefa: Tarefa) {
    this.tarefaSelecionada = tarefa;
    this.form.patchValue(tarefa);
    this.mostrarJanelaEditar = true;
  }

  togglePrioridade() {
    this.openPrioridade = !this.openPrioridade;
  }

  toggleStatus() {
    this.openStatus = !this.openStatus;
  }

  selectPrioridade(item: string) {
    this.form.get('prioridade')?.setValue(item);
    this.openPrioridade = false;
  }
  selectStatus(item: string) {
    this.form.get('status')?.setValue(item);

    this.openStatus = false;
  }

  getProjetoById(id: number) {
    this.projetoService.getProjetoById(id).subscribe((projeto) => {
      this.projetoSelecionado = projeto;
    });
  }

  atualizarTarefa(id: number) {
    if (this.form.valid && this.projetoSelecionado?.id) {
      const tarefa: Tarefa = {
        ...this.form.value,
        projetoId: this.projetoSelecionado.id,
      };

      this.tarefaService.atualizarTarefa(tarefa, id).subscribe({
        next: () => {
          this.snackBar.open('Tarefa atualizada com sucesso', 'Fechar', {
            duration: 3000,
            panelClass: 'sucess-snackbar',
            horizontalPosition: 'center',
            verticalPosition: 'top',
          });
          this.mostrarJanelaEditar = false;
          this.refresh$.next();
          this.form.reset();
        },
        error: (err) => {
          this.snackBar.open('Já existe uma tarefa com esse nome', 'Fechar', {
            duration: 3000,
            panelClass: 'erro-snackbar',
            horizontalPosition: 'center',
            verticalPosition: 'top',
          });
        },
      });
    }
  }

  criarTarefa() {
    if (this.form.valid && this.projetoSelecionado?.id) {
      const tarefa: Tarefa = {
        ...this.form.value,
        projetoId: this.projetoSelecionado.id,
      };

      this.tarefaService.setTarefa(tarefa).subscribe({
        next: () => {
          this.isOpen = false;
          this.refresh$.next();
          this.form.reset();
          this.snackBar.open('Tarefa criada com sucesso', 'Fechar', {
            duration: 3000,
            panelClass: 'sucess-snackbar',
            horizontalPosition: 'center',
            verticalPosition: 'top',
          });
        },
        error: (err) => {
          this.snackBar.open('Já existe uma tarefa com esse nome', 'Fechar', {
            duration: 3000,
            panelClass: 'erro-snackbar',
            horizontalPosition: 'center',
            verticalPosition: 'top',
          });
        },
      });
    }
  }

  excluirTarefa(tarefaEscolhida: Tarefa) {
    if (tarefaEscolhida) {
      this.tarefaService.deleteTarefa(tarefaEscolhida, this.idLink).subscribe({
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
}
