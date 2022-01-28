import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';
import {ConfirmComponent} from './confirm/confirm.component';
import {ResetComponent} from './reset/reset.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent, data: {title: 'Login:: Appraisal Portal'}
  },
  {
    path: 'register',
    component: RegisterComponent, data: {title: 'Register:: Appraisal Portal'}
  },
  {
    path: 'confirm',
    component: ConfirmComponent, data: {title: 'Confirm:: Appraisal Portal'}
  },
  {
    path: 'reset-password',
    component: ResetComponent, data: {title: 'Reset Password:: Appraisal Portal'}
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
