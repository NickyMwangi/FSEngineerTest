import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import {LayoutsComponent} from './layouts/layouts.component';
import {AuthGaurdService} from './utility/guards/auth-gaurd.service';
import {AuthService} from './utility/guards/auth.service';

const routes: Routes = [
  { path: 'account',  loadChildren: () => import('./account/account.module').then(m => m.AccountModule) },
  {
    path: '', component: LayoutsComponent, loadChildren: () => import('./areas/areas.module')
      .then(m => m.AreasModule), canActivate: [AuthGaurdService]
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {scrollPositionRestoration: 'top'})],
  exports: [RouterModule]
})
export class AppRoutingModule {
  constructor(private service: AuthService) {
  }
}
