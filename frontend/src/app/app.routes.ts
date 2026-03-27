import { Routes } from '@angular/router';
import path from 'path';
import { TelaLogin } from './components/tela-login/tela-login';
import { TelaRegister } from './components/tela-register/tela-register';
import { TelaDashboard } from './components/tela-dashboard/tela-dashboard';
import { TelaProjetos } from './components/tela-projetos/tela-projetos';
import { MainLayoutComponent } from './layout/main-layout-component/main-layout-component';
import { AuthLayoutComponent } from './layout/auth-layout-component/auth-layout-component';

export const routes: Routes = [
    {
    path: '',
    component: AuthLayoutComponent,
    children: [
      { path: '', component: TelaLogin },
      { path: 'login', component: TelaLogin },
      { path: 'register', component: TelaRegister }
    ]
  },

  // 🔒 Rotas privadas
  {
    path: '',
    component: MainLayoutComponent,
    children: [
      { path: 'dashboard', component: TelaDashboard },
      { path: 'projetos', component: TelaProjetos }
    ]
  }
];
