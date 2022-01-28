import { Injectable } from '@angular/core';
import {environment} from '../../environments/environment';
import {ToastrService} from 'ngx-toastr';
import {HttpClient, HttpParams} from '@angular/common/http';
import {Subject, throwError} from 'rxjs';
import Swal from 'sweetalert2';

interface SubjectMap {
  [tableName: string]: {
    many: Subject<any[]>;
    one: Subject<any>;
  };
}

interface Cache {
  [tableName: string]: any[];
}


interface TableCRUD {
  cache: Cache;
  subjectMap: SubjectMap;
}

@Injectable({
  providedIn: 'root'
})
export class EventService {
  public API_URL = environment.API_ENDPOINT;
  public CLIENT_URL = environment.CLIENT_ENDPOINT;

  constructor(private http: HttpClient, private toastr: ToastrService) {
  }

  // Account Urls
  get LOGIN_URL(): string { return this.API_URL + 'Account/Login/'; }
  // Areas Urls
  get JokesURL(): string { return this.API_URL + 'jokes/'; }
  get CategoryURL(): string { return this.API_URL + 'jokes/category/'; }
  get PeopleURL(): string { return this.API_URL + 'api/people/'; }

  cache: any = {};
  subjectMap: SubjectMap = {};

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

  public handleHttpError = n => {
    let errorMessage = '';
    if (n.error instanceof ErrorEvent) {
      // client-side error
      errorMessage = `An error occurred: ${n.error.message}`;
    } else {
      // server-side error
      errorMessage = `Server error occurred: ${n.title}, with error message: ${n.errors}`;
    }
    Swal.fire('Error!', errorMessage, 'error');
    return throwError(errorMessage);
  }

  public handlePostError = n => {
    let errorMessage = '';
    if (n.error instanceof ErrorEvent) {
      // client-side error
      errorMessage = `An error occurred: ${n.errors}`;
    } else {
      // server-side error
      errorMessage = `Server error occurred while processing the request`;
    }
    return throwError(errorMessage);
  }

  public cacheAndNotifyRead = <T>(model: T | any, res: T[]) => {
    this.cache[model.tableName] = [];
    res.forEach((el: T) => {
      this.cache[model.tableName].push(new model(el));
    });
    console.log(this.subjectMap.AspNetUsers);
    // Update Frontend
    this.subjectMap.AspNetUsers.many.next(this.cache[model.tableName]);
  }
}
