import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {ProfileComponent} from './profile/profile.component';
import {AppComponent} from '../app.component';
import {UsersComponent} from './users/users.component';
import {MenusComponent} from './menus/menus.component';
import {GoalsComponent} from './goals/goals.component';
import {PeriodComponent} from './period/period.component';
import {AppraisalComponent} from './appraisal/appraisal.component';
import {AppCreateComponent} from './appraisal/app-create/app-create.component';
import {ChildsComponent} from './childs/childs.component';

const routes: Routes = [
  {path: '', component: ProfileComponent, data: {title: 'Profile:: Appraisal Portal'}},
  {path: 'profile', component: ProfileComponent, data: {title: 'Profile:: Appraisal Portal'}},
  {path: 'appraisal', component: ChildsComponent, data: {title: 'Appraisal:: Appraisal Portal'},
    children: [
      { path: '', component: AppraisalComponent, data: { title: 'Appraisal:: Appraisal Portal'}},
      { path: 'create', component: AppCreateComponent, data: { title: 'Create Appraisal:: Appraisal Portal'} },
      { path: 'edit/:id', component: AppCreateComponent, data: { title: 'Edit Appraisal:: Appraisal Portal'} },
    ]
  },
  {
    path: 'admin', component: AppComponent, data: {title: 'Admin:: Appraisal Portal'},
    children: [
      {path: 'users', component: UsersComponent, data: {title: 'users:: Appraisal Portal'}},
      {path: 'menus', component: MenusComponent, data: {title: 'Menus:: Appraisal Portal'}},
    ]
  },
  {
    path: 'setups', component: ChildsComponent, data: {title: 'Setups:: Appraisal Portal'},
    children: [
      {path: 'goals', component: GoalsComponent, data: {title: 'Goals:: Appraisal Portal'}},
      {path: 'stages', component: PeriodComponent, data: {title: 'Period :: Appraisal Portal'}},
    ]
  }
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AreasRoutingModule {
}
