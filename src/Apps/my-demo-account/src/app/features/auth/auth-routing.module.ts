import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from './pages/login/login.component';
import {RegisterComponent} from './pages/register/register.component';
import {ForgotPasswordComponent} from './pages/forgot-password/forgot-password.component';
import {PasswordResetComponent} from './pages/password-reset/password-reset.component';
import {NgModule} from '@angular/core';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },
  {
    path: 'login',
    component: LoginComponent,
    title: 'Login'
  },
  {
    path: 'register',
    component: RegisterComponent,
    title: 'Register'
  },
  {
    path: 'forgot-password',
    component: ForgotPasswordComponent,
    title: 'Forgot Password'
  },
  {
    path: 'password-reset',
    component: PasswordResetComponent,
    title: 'Password Reset'
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
