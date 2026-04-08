import { Routes } from '@angular/router';
import { ChatComponent } from './components/chat/chat';

export const routes: Routes = [
  { path: '', component: ChatComponent }, // Ruta por defecto al entrar a la página
  { path: '**', redirectTo: '' } // Redirige al chat si escriben cualquier otra URL
];
