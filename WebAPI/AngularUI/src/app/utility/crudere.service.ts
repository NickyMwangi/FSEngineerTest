import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Observable, Subject, throwError} from 'rxjs';
import {catchError, map, shareReplay, tap} from 'rxjs/operators';
import Swal from 'sweetalert2';
import {UrlService} from '../services/url.service';
import {ToastrService} from 'ngx-toastr';
import {NgxSpinnerService} from 'ngx-spinner';
import {IUserModel, SessionData} from '../iModels/iuser-model';
import {CookieService} from './guards/cookie.service';
import {Router} from '@angular/router';
import {EventService} from '../services/event.service';

@Injectable({
  providedIn: 'root'
})

export abstract class CrudereService {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Accept: '*/*',
    }),
    params: new HttpParams(),
  };
  identityUser: SessionData;

  protected constructor(
    protected httpCli: HttpClient,
    private n: EventService,
    private router: Router,
    private toastr: ToastrService,
    private cookie: CookieService
  ) { }

  Identity(): IUserModel {
    const currentUser = JSON.parse(this.cookie.getCookie('UserToken'));
    if (currentUser) {
      this.identityUser = currentUser;
    }
    return this.identityUser?.user;
  }

  createObs<T>(model: T | any, objToCreate?: T | any, path?: string): Observable<T | T[]> {
    const newModelObj = new model(objToCreate);
    const body = JSON.stringify(objToCreate);
    return this.httpCli.post<T[]>(path, body, this.httpOptions).pipe(
      tap((res: T[] | any) => {
        Swal.fire('Saving Successful', 'Details Saved Successfully', 'success');
        newModelObj.key = res.key || res.ObjectId || res.id || '';
      }),
      catchError(this.n.handlePostError)
    );
  }
  readObs<T>(query?: HttpParams | string | any, path?: string | any): Observable<T> {
    const httpOpts = Object.assign({}, this.httpOptions);
    if (query) {
      httpOpts.params = this.n.createSearchParams(query);
    }
    return this.httpCli.get<T>(path, httpOpts).pipe(
      tap((m: T) => m),
      shareReplay(),
      catchError(this.n.handleHttpError)
    );
  }
  updateObs<T>(
    model: T | any,
    objToUpdate?: T | any,
    path?: string
  ): Observable<T | T[]> {
    const body = JSON.stringify(objToUpdate);
    const newModelObj = new model(objToUpdate);
    return this.httpCli.put<T[]>(path, body, this.httpOptions).pipe(
      tap((res: T[] | any) => {
        Swal.fire(
          'Saving Successful',
          'Details updated Successfully',
          'success'
        );
        newModelObj.key = res.key || res.ObjectId || res.id || '';
      }),
      catchError(this.n.handlePostError)
    );
  }

  Delete<T>(path: string): Observable<T[]> {
    return this.httpCli.delete<T[]>(path, this.httpOptions);
  }

  whereObs<T>(exp?: string | any, url?: string | any): Observable<T> {
    const Opts = Object.assign({}, this.httpOptions);
    return this.httpCli.get<T>(url + exp + '/filtered', Opts).pipe(
      tap((m: T) => m),
      shareReplay(),
      catchError(this.n.handleHttpError)
    );
  }

}
