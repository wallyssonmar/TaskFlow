import { Injectable } from '@angular/core';
import { Tarefa } from '../models/tarefa';
import { HttpClient } from '@angular/common/http';
import { Observable, } from 'rxjs';
import { TarefasResponse } from '../models/tarefas-response';

@Injectable({
  providedIn: 'root',
})
export class TarefaService {
  
  private apiUrl = 'https://localhost:7133/api/Tarefa';
 

  constructor(private http: HttpClient) {}

  getTarefas(id : number): Observable<TarefasResponse>{
    return this.http.get<TarefasResponse>(`${this.apiUrl}/${id}`)
   
  }

  setTarefa(tarefa: Tarefa) {
    
    return this.http.post<Tarefa>(this.apiUrl, tarefa)
  }

  deleteTarefa(tarefa: Tarefa, id : number){
    return this.http.delete<Tarefa>(`${this.apiUrl}/${id}/${tarefa.id}`)
  }

  atualizarTarefa(tarefa: Tarefa, id: number) {
    console.log(id)
    return this.http.put<Tarefa>(`${this.apiUrl}/${tarefa.projetoId}/${id}`, tarefa)
  }
}
