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
import { Observable, startWith, Subject, switchMap } from 'rxjs';
import { TarefasResponse } from '../../models/tarefas-response';

@Component({
  selector: 'app-tela-tarefa',
  imports: [RouterLink, DragDropModule, CommonModule, ReactiveFormsModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  templateUrl: './tela-tarefa.html',
  styleUrl: './tela-tarefa.css',
})
export class TelaTarefa {
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
  projetoSelecionado: Projeto | null = null;
  private refresh$ = new Subject<void>();

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
    this.route.params.subscribe((params) => {
      const id = Number(params['id']);
      this.idLink = id;
      this.projetoService.getProjetoById(id).subscribe((projeto) => {
        this.projetoSelecionado = projeto;
      });
      
    });
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


  criarTarefa() {
    if (this.form.valid && this.projetoSelecionado?.id) {
      const tarefa: Tarefa = {
        ...this.form.value,

        projetoId: this.projetoSelecionado.id,
      };
      console.log(tarefa);
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
}
