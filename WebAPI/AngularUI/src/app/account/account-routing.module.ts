import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LoginComponent} from './login/login.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent, data: {title: 'Login:: SovTech FS Engineer Task'}},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AccountRoutingModule { }
