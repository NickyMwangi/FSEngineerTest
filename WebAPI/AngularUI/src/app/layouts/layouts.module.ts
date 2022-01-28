import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutsComponent } from './layouts.component';
import { FooterComponent } from './footer/footer.component';
import { TopbarComponent } from './topbar/topbar.component';
import {RouterModule} from '@angular/router';
import {NgbDropdownModule, NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { SidebarComponent } from './sidebar/sidebar.component';
import { LoaderComponent } from './loader/loader.component';

@NgModule({
  declarations: [
    LayoutsComponent,
    FooterComponent,
    TopbarComponent,
    SidebarComponent,
    LoaderComponent,
  ],
  imports: [
    CommonModule,
    RouterModule,
    NgbDropdownModule,
  ],
})
export class LayoutsModule { }
