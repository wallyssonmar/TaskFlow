import { Component, CUSTOM_ELEMENTS_SCHEMA, NgZone } from '@angular/core';
import { ProjetoService } from '../../services/projeto-service';
import { Projeto } from '../../models/projeto';
import { ActivatedRoute, RouterLink } from '@angular/router';
import {
  CdkDragDrop,
  DragDropModule,
  moveItemInArray,
  transferArrayItem,
} from '@angular/cdk/drag-drop';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { TarefaService } from '../../services/tarefa-service';
import { Tarefa } from '../../models/tarefa';
import { map, Observable, startWith, Subject, switchMap, tap } from 'rxjs';
import { TarefasResponse } from '../../models/tarefas-response';
import { error } from 'node:console';

@Component({
  selector: 'app-tela-tarefa',
  imports: [RouterLink, DragDropModule, CommonModule, ReactiveFormsModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  templateUrl: './tela-tarefa.html',
  styleUrl: './tela-tarefa.css',
})
export class TelaTarefa {
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
    switchMap(() => this.tarefaService.getTarefas(this.idLink)),
  );

  constructor(
    private projetoService: ProjetoService,
    private route: ActivatedRoute,
    private tarefaService: TarefaService,
    private fb: FormBuilder,
    private zone: NgZone,
  ) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      description: ['', [Validators.required, Validators.maxLength(65)]],
      prioridade: ['', Validators.required],
      status: ['', Validators.required],
    });
  }

  ngOnInit() {
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
    if (this.form.valid && this.projetoSelecionado?.id){
      const tarefa: Tarefa = {
        ...this.form.value,
        projetoId: this.projetoSelecionado.id,
      };
      
      this.tarefaService.atualizarTarefa(tarefa, id).subscribe({
        next: () => {
          this.mostrarJanelaEditar = false;
          this.refresh$.next();
          this.form.reset();
        },
        error: (err) => {
          console.error('Erro ao editar a tarefa:', err);
        },
      })
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
        },
        error: (err) => {
          console.error('Erro ao criar tarefa:', err);
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
          console.log(err.error.errors);
        },
      });
    }
  }
}
