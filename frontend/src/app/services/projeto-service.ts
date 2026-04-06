import { Injectable } from '@angular/core';
import { Projeto } from '../models/projeto';
import { of,Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ProjetoService {
  tarefaEscolhido: any;
  
  projetos : Projeto [] = [
    {name: 'Teste 1', descricao: 'Testando o tamanho da descrição do projeto para melhora', qtdTarefa: 4,qtdTarefaConcluida: 2,qtdMembros: 3,selectedColor: '#3B82F6'},
    {name: 'Teste 2', descricao: 'Testando o tamanho da descrição do projeto para melhora', qtdTarefa: 2,qtdTarefaConcluida: 1,  qtdMembros: 4,selectedColor: '#EF4444'},
    {name: 'Teste 3', descricao: 'Testando o tamanho da descrição do projeto para melhora', qtdTarefa: 6,qtdTarefaConcluida: 4,  qtdMembros: 5,selectedColor: '#22C55E'},
    {name: 'Teste 4', descricao: 'Testando o tamanho da descrição do projeto para melhora', qtdTarefa: 23,qtdTarefaConcluida: 20, qtdMembros: 42,selectedColor: '#8B5CF6'},
    {name: 'Teste 5', descricao: 'Testando o tamanho da descrição do projeto para melhora', qtdTarefa: 21,qtdTarefaConcluida: 21, qtdMembros: 1,selectedColor: '#1F2937'},
  ]
  
  
  getProjetos(){
    return this.projetos.map(p => ({
      ...p,
      progresso: p.qtdTarefa ? (p.qtdTarefaConcluida / p.qtdTarefa) * 100 : 0
    }));
  }

  getProjetoEscolhido(): Observable<Projeto>{
    return of(this.tarefaEscolhido);
  }
  setProjeto(projeto : Projeto){
    this.projetos.push(projeto);
    console.log(projeto)
  }

  setTarefa(projeto: Projeto) {
    
    return this.tarefaEscolhido = projeto

  }

}

