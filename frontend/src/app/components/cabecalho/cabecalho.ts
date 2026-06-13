import { Component, inject, PLATFORM_ID } from '@angular/core';
import { UserService } from '../../services/user-service';
import { Observable } from 'rxjs';
import { User } from '../../models/user';
import { AsyncPipe, isPlatformBrowser } from '@angular/common';

@Component({
  selector: 'app-cabecalho',
  imports: [AsyncPipe],
  templateUrl: './cabecalho.html',
  styleUrl: './cabecalho.css',
})
export class Cabecalho {
  pessoaLogada$!: Observable<User | null>;
  private platformId = inject(PLATFORM_ID);
  constructor(private userService: UserService) {
    this.pessoaLogada$ = this.userService.currentUser$;
  }
  ngOnInit() {
    if (isPlatformBrowser(this.platformId) && localStorage.getItem('token')) {
      this.userService.getCurrentUser().subscribe((user) => {
        this.userService.setCurrentUser(user);
      });
    }
  }
}
