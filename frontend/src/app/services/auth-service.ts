import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Register } from '../models/register';
import { Login } from '../models/login';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  
  private apiUrl = 'https://localhost:7133/api/Auth'

  constructor(private http: HttpClient) {}

  setRegisterUser(registerUser: Register){
    console.log(registerUser)  
    return this.http.post<Register>(`${this.apiUrl}/register`,registerUser)
  }
  VerificarLogin(login: Login) {
    return this.http.post<Login>(`${this.apiUrl}/login`,login)
  }
}
