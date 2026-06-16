import { Component, inject, PLATFORM_ID } from '@angular/core';
import { UserService } from '../../services/user-service';
import { Observable } from 'rxjs';
import { User } from '../../models/user';
import { AsyncPipe, CommonModule, isPlatformBrowser } from '@angular/common';
import { AuthService } from '../../services/auth-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cabecalho',
  imports: [AsyncPipe, CommonModule],
  templateUrl: './cabecalho.html',
  styleUrl: './cabecalho.css',
})
export class Cabecalho {
  mostrarConfirmacao = false;
  pessoaLogada$!: Observable<User | null>;
  private platformId = inject(PLATFORM_ID);
  constructor(
    private userService: UserService,
    private authService: AuthService,
    private router: Router,
  ) {
    this.pessoaLogada$ = this.userService.currentUser$;
  }
  ngOnInit() {
    if (isPlatformBrowser(this.platformId) && localStorage.getItem('token')) {
      this.userService.getCurrentUser().subscribe((user) => {
        this.userService.setCurrentUser(user);
      });
    }
  }

  logout() {
    this.authService.logout().subscribe({
      next: () => {
        localStorage.removeItem('token');
        localStorage.removeItem('refreshToken');
        this.router.navigate(['/login']);
      },
    });
  }
}
