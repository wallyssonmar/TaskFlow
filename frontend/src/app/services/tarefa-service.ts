import { Injectable } from '@angular/core';
import { Tarefa } from '../models/tarefa';

@Injectable({
  providedIn: 'root',
})
export class TarefaService {
  tarefas: Tarefa[] = [
    { name: 'Tarefa 1', descricao: 'Oloko', prioridade: 'Média', status: 'A Fazer' },
    { name: 'Tarefa 2', descricao: 'dadasdadadaaa', prioridade: 'Alta', status: 'Em progresso' },
    { name: 'Tarefa 3', descricao: 'loriem', prioridade: 'Média', status: 'A Fazer' },
    { name: 'Tarefa 4', descricao: 'Olokoaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa', prioridade: 'Baixa', status: 'Concluído' },
  ];

  getTarefas(){
    
    return {
      afazer: this.tarefas.filter(t => t.status === 'A Fazer'),
      progresso: this.tarefas.filter(t => t.status === 'Em progresso'),
      concluido: this.tarefas.filter(t => t.status === 'Concluído')
      
    }
  }

  setTarefa(tarefa: Tarefa){
    this.tarefas = [...this.tarefas, tarefa];
    
  }
}

