import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Register } from '../models/register';
import { Login } from '../models/login';
import { response } from 'express';
import { LoginReponse } from '../models/Login-reponse';
import { BehaviorSubject, tap } from 'rxjs';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private apiUrl = 'https://localhost:7133/api/Auth';

  constructor(private http: HttpClient) {}

  setRegisterUser(registerUser: Register) {
    console.log(registerUser);
    return this.http.post<Register>(`${this.apiUrl}/register`, registerUser);
  }
  VerificarLogin(login: Login) {
    return this.http.post<LoginReponse>(`${this.apiUrl}/login`, login).pipe(
      tap((response) => {
        localStorage.setItem('token', response.token);
        localStorage.setItem('refreshToken', response.refreshToken);
        console.log(response);
      }),
    );
  }

  refreshToken(refreshToken: string) {
    return this.http.post<LoginReponse>(`${this.apiUrl}/refresh-token`, { refreshToken });
  }
}
