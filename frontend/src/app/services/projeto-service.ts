import { Injectable } from '@angular/core';
import { Projeto } from '../models/projeto';
import { of,Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class ProjetoService {
  private apiUrl = 'https://localhost:7133/api/Projeto'

  tarefaEscolhido: any;
  
  projetos : Projeto [] = []
  /**
   *
   */
  constructor(private http: HttpClient) {}

  
  
  getProjetos(){
    return this.http.get<Projeto[]>(this.apiUrl);
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

