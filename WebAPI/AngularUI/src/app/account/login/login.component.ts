import {AfterViewInit, Component, OnDestroy, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {ActivatedRoute, Router} from '@angular/router';
import {first} from 'rxjs/operators';
import {AuthService} from '../../utility/guards/auth.service';
import {Observable} from 'rxjs';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {
  isLoading$: Observable<boolean>;
  isLoggediIn$: Observable<boolean>;
  isErrorMsg$: Observable<Error>;
  hasError$: Observable<boolean>;
  loginForm: FormGroup;
  returnUrl: string;
  today = new Date();
  submit = false;

  constructor(private formBuilder: FormBuilder, private route: ActivatedRoute, private router: Router,
              public auth: AuthService) { }

  ngOnInit(): void {
    this.defineForm();
  }

  defineForm(): any {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
    this.returnUrl = this.route.snapshot.queryParams.returnUrl || '/';
  }
  // convenience getter for easy access to form fields
  get f(): any { return this.loginForm.controls; }

  ngOnDestroy(): void {
  }

}
