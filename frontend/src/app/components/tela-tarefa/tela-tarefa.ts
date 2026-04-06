import { Component, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ProjetoService } from '../../services/projeto-service';
import { Projeto } from '../../models/projeto';
import { RouterLink } from '@angular/router';
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

@Component({
  selector: 'app-tela-tarefa',
  imports: [RouterLink, DragDropModule, CommonModule, ReactiveFormsModule],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  templateUrl: './tela-tarefa.html',
  styleUrl: './tela-tarefa.css',
})
export class TelaTarefa {
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

  constructor(
    private projetoService: ProjetoService,
    private tarefaService: TarefaService,
    private fb: FormBuilder,
  ) {
    this.form = this.fb.group({
      name: ['', Validators.required],
      descricao: ['', [Validators.required, Validators.maxLength(65)]],
      prioridade: ['', Validators.required],
      status: ['', Validators.required],
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

  projetoSelecionado: Projeto | null = null;
  ngOnInit() {
    this.projetoService.getProjetoEscolhido().subscribe((p) => {
      this.projetoSelecionado = p;
    });
    this.getTarefaPorStatus();
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

  getTarefaPorStatus() {
    const grupos = this.tarefaService.getTarefas();

    this.listaAfazers = grupos.afazer;
    this.listaProgressos = grupos.progresso;
    this.listaConcluidas = grupos.concluido;
    console.log("Alo")
  }

  criarTarefa() {
    if (this.form.valid) {
      
      this.tarefaService.setTarefa(this.form.value);
      this.isOpen = false;
      this.getTarefaPorStatus();
    }
  }
}
