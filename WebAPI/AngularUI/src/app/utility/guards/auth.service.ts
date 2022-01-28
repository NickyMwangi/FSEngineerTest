import { Injectable } from '@angular/core';
import { CookieService } from './cookie.service';
import {HttpClient} from '@angular/common/http';
import {UrlService} from '../../services/url.service';
import {AngularFireAuth} from '@angular/fire/auth';
import {Router} from '@angular/router';
import Swal from 'sweetalert2';
import {FormGroup} from '@angular/forms';
import {NgxSpinnerService} from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  UserModel: firebase.User;
  constructor(private http: HttpClient, private cookieService: CookieService, private url: UrlService,
              private afAuth: AngularFireAuth, private router: Router, private sp: NgxSpinnerService) {
    this.afAuth.authState.subscribe(user => {
      sp.show();
      if (user) {
        this.UserModel = user;
        this.cookieService.setCookie('CookieUser', JSON.stringify(this.UserModel), 1);
        sp.hide();
      }
      else  {
        this.cookieService.deleteCookie('CookieUser');
        this.cookieService.setCookie('CookieUser', JSON.stringify(null), 1);
        sp.hide();
      }
    });
  }
  /**
   * Returns the current user
   */
  public currentToken() {
    if (!this.UserModel) {
      this.UserModel = JSON.parse(this.cookieService.getCookie('CookieUser'));
    }
    return this.UserModel;
  }

  logIn(email: string, password: string, form: FormGroup){
    this.sp.show();
    if (form.valid) {
      return this.afAuth.signInWithEmailAndPassword(email, password)
        .then((result) => {
          this.UserModel = result.user;
          this.cookieService.setCookie('CookieUser', JSON.stringify(this.UserModel), 1);
          this.router.navigate(['/profile']);
          this.sp.hide();
        }).catch((error) => {
          this.sp.hide();
          Swal.fire('Login Error', error.message, 'error');
        });
    }
    else  {
      this.sp.hide();
      Swal.fire('Login Error', 'Invalid credentials', 'error');
    }
  }

  signUp(email: string, password: string) {
    this.sp.show();
    return this.afAuth.createUserWithEmailAndPassword(email, password)
      .then((result) => {
        this.SendVerificationMail().then(r => {
          this.router.navigate(['/account/login']);
          this.sp.hide();
          Swal.fire('Success', 'Registration successful. Check your email address for confirmation', 'success');
        });
      }).catch((error) => {
        this.sp.hide();
        Swal.fire('Error', error.message, 'error');
      });
  }

  SendVerificationMail() {
    return this.afAuth.currentUser.then(u => u.sendEmailVerification())
      .then(() => {
        this.router.navigate(['/account/confirm']);
      });
  }

  ForgotPassword(passwordResetEmail) {
    this.sp.show();
    return this.afAuth.sendPasswordResetEmail(passwordResetEmail)
      .then(() => {
        window.alert('Password reset email sent, check your inbox.');
        this.sp.hide();
      }).catch((error) => {
        Swal.fire('Login Error', error.message, 'error');
        this.sp.hide();
      });
  }

  // Returns true when user is looged in and email is verified
  get isLoggedIn(): boolean {
    const user = JSON.parse(localStorage.getItem('user'));
    return (user !== null && user.emailVerified !== false);
  }

  /**
   * Logout the user
   */
  logout(){
    this.sp.show();
    return this.afAuth.signOut().then(() => {
      this.cookieService.deleteCookie('CookieUser');
      this.router.navigate(['/account/login']);
      this.UserModel = null;
      this.sp.hide();
    });
  }
}
