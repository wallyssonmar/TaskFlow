import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Register } from '../models/register';

@Injectable({
  providedIn: 'root',
})
export class RegisterService {
  private apiUrl = 'https://localhost:7133/api/Auth'

  constructor(private http: HttpClient) {}

  setRegisterUser(registerUser: Register){
    console.log(registerUser)  
    return this.http.post<Register>(`${this.apiUrl}/register`,registerUser)
  }
}
