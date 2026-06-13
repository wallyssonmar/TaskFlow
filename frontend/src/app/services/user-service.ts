import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(private http: HttpClient) {}

  private apiUrl = 'https://localhost:7133/api/User';
  private currentUserSubject = new BehaviorSubject<User | null>(null);

  currentUser$ = this.currentUserSubject.asObservable();

  setCurrentUser(user: User) {
    this.currentUserSubject.next(user);
  }

  getCurrentUser() {
    return this.http.get<User>(this.apiUrl);
  }

  getCurrentUserValue() {
    return this.currentUserSubject.value;
  }
}
