import { Injectable } from '@angular/core';
import { CookieService } from './cookie.service';
import {HttpClient, HttpHeaders, HttpParams} from '@angular/common/http';
import {Router} from '@angular/router';
import {SessionData} from '../../iModels/iuser-model';
import {EventService} from '../../services/event.service';
import Swal from 'sweetalert2';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
      Accept: '*/*'
    }),
    params: new HttpParams()
  };
  userModel: SessionData;

  constructor(private http: HttpClient, private cookieService: CookieService, private n: EventService, private r: Router) {
  }

  currentToken(): SessionData {
    if (!this.userModel) {
      this.userModel = JSON.parse(this.cookieService.getCookie('UserToken'));
    }
    return this.userModel;
  }

  login(Email: string, Password: string) {
    this.http.post(this.n.LOGIN_URL, { Email, Password }, this.httpOptions)
      .subscribe((n: SessionData) => {
        this.userModel = n;
        this.cookieService.setCookie('UserToken', JSON.stringify(n), 1);
        this.r.navigateByUrl('/');
    }, (err) => {
      Swal.fire('Login', err);
    });
  }

  /**
   * Logout the user
   */
  logout = () => {
    // remove user from local storage to log user out
    this.cookieService.deleteCookie('UserToken');
    this.userModel = null;
    this.r.navigateByUrl('/');
    window.location.reload();
    // return this.http.post(this.n.LOGOUT_URL, '');
  }
}
