import { Routes } from '@angular/router';
import { HomeComponent } from './pages/home/home';
import { Oauth } from './components/oauth/oauth';

export const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'home', component: HomeComponent },
  { path: 'auth-callback', component: Oauth }, 
  { path: '**', redirectTo: '' }
];