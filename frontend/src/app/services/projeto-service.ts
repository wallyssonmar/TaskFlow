import { Injectable } from '@angular/core';
import { Projeto } from '../models/projeto';
import { of,Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Tarefa } from '../models/tarefa';
import { ProcessEnvOptions } from 'node:child_process';

@Injectable({
  providedIn: 'root',
})
export class ProjetoService {
  
  private apiUrl = 'https://localhost:7133/api/Projeto'
  

  projetoEscolhido: Projeto | null = null;
  
  projetos : Projeto [] = []
  /**
   *
   */
  constructor(private http: HttpClient) {}

  
  
  getProjetos(){
    return this.http.get<Projeto[]>(this.apiUrl);
  }

  getProjetoById(id: number): Observable<Projeto>{
    return this.http.get<Projeto>(`${this.apiUrl}/${id}`);
  }
  setProjeto(projeto : Projeto){
   return this.http.post<Projeto>(this.apiUrl,projeto);
    
  }
  deleteProjeto(id: number,) {
    return this.http.delete<Projeto>(`${this.apiUrl}/${id}`);
  }
  editarProjeto(id: number, projeto: Projeto){
    return this.http.put<Projeto>(`${this.apiUrl}/${id}`, projeto);
  }

  

  
}

