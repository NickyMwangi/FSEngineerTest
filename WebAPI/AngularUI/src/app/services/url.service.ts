import { Injectable } from '@angular/core';
import {HttpClient, HttpParams} from '@angular/common/http';
import Swal from 'sweetalert2';
import {throwError} from 'rxjs';
import {ToastrService} from 'ngx-toastr';
import {NgxSpinnerService} from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})


export class UrlService {
  public API_URL = 'https://localhost:44380/';
  get LOGIN_URL(): string  { return this.API_URL + 'Account/Login'; }
  get REGISTER_URL(): string { return this.API_URL + 'Account/Register'; }
  get LOGOUT_URL(): string  { return this.API_URL + 'Account/Logout'; }

  cache: any = {};
  constructor(private http: HttpClient, private toaster: ToastrService, private sp: NgxSpinnerService) {}

  public createSearchParams(query: HttpParams | string | any): HttpParams {
    let newParams: HttpParams;
    if (typeof query === 'string') {
      let searchParams = new HttpParams();
      const splitQuery = query.split('&');
      splitQuery.forEach(param => {
        const keyValPair = param.split('=');
        searchParams = searchParams.set(keyValPair[0], keyValPair[1]);
      });
      newParams = searchParams;
    } else if (query instanceof HttpParams) {
      newParams = query;
    } else {
      // Parse object into HttpParams
      Object.keys(query).forEach((key) => {
        newParams = newParams.set(key, query[key]);
      });
    }
    return newParams;
  }

  public handleHttpError(n) {
    this.sp.show();
    let errorMessage = '';
    if (n.error instanceof ErrorEvent) {
      // client-side error
      errorMessage = `An error occurred: ${n.error.message}`;
    } else {
      // server-side error
      errorMessage = `Server error occurred: ${n.title}, with error message: ${n.errors}`;
    }
    console.log(errorMessage);
    Swal.fire('Error!', errorMessage, 'error');
    this.sp.hide();
    return throwError(errorMessage);
  }

  public handlePostError(n) {
    this.sp.show();
    let errorMessage = '';
    if (n.error instanceof ErrorEvent) {
      // client-side error
      errorMessage = `An error occurred: ${n.error.message}`;
    } else {
      // server-side error
      errorMessage = `Server error occurred: ${n}, with error message: ${n.errors}`;
    }
    console.log(errorMessage);
    this.toaster.error('Saving Failed' + errorMessage, 'Error');
    this.sp.hide();
    return throwError(errorMessage);
  }

}
