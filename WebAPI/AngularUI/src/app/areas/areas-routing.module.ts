import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {CategoryComponent} from './category/category.component';
import {JokesComponent} from './jokes/jokes.component';
import {PeopleComponent} from './people/people.component';
import {MenusComponent} from './menus/menus.component';

const routes: Routes = [
  {path: '', component: CategoryComponent, data: {title: ' Home :: SovTech FS Engineer Task'}},
  {path: 'category', component: CategoryComponent, data: {title: 'Category :: SovTech Task'}},
  {
    path: 'jokes',
    children: [
      { path: '', component: JokesComponent },
      { path: ':category', component: JokesComponent },
    ]
  },
  {path: 'people', component: PeopleComponent, data: {title: 'people :: SovTech Task'}},
  {path: 'search', component: MenusComponent, data: {title: 'Search :: SovTech Task'}},
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AreasRoutingModule {
}
