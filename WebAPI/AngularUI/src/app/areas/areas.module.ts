import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CategoryComponent } from './category/category.component';
import { JokesComponent } from './jokes/jokes.component';
import { PeopleComponent } from './people/people.component';
import {TitleComponent} from '../shared/title/title.component';
import {RouterModule} from '@angular/router';
import {MenusComponent} from './menus/menus.component';
import {UsersComponent} from './users/users.component';
import {AreasRoutingModule} from './areas-routing.module';
@NgModule({
    declarations: [CategoryComponent, JokesComponent, PeopleComponent, TitleComponent, MenusComponent, UsersComponent],
  imports: [
    CommonModule,
    RouterModule,
    AreasRoutingModule
  ]
})
export class AreasModule { }
