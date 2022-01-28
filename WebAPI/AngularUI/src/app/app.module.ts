import {BrowserModule} from '@angular/platform-browser';
import {CUSTOM_ELEMENTS_SCHEMA, NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {ToastrModule} from 'ngx-toastr';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {LayoutsModule} from './layouts/layouts.module';
import {NgHttpLoaderModule} from 'ng-http-loader';
import {JwtInterceptor} from './utility/helpers/jwt.interceptor';
import {ErrorInterceptor} from './utility/helpers/error.interceptor';
import {IndexServices} from './services/index.service';
import {environment} from '../environments/environment';
import {NgxSpinnerModule} from 'ngx-spinner';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule, BrowserAnimationsModule, HttpClientModule, LayoutsModule, AppRoutingModule, NgHttpLoaderModule.forRoot(),
    ToastrModule.forRoot({progressBar: true, positionClass: 'toast-top-center'}),
    NgxSpinnerModule,
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true}, IndexServices
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
