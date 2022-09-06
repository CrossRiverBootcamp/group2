import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/Login';
import { Register } from 'src/app/models/Register';
import { AccountService } from 'src/app/services/account.service';
import { UserService } from 'src/app/services/user.service';
import { MustMatch } from '../helpers/MustMatch.validator';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  submitted: boolean = false;
  showError: boolean = false;
  loading: boolean = false;

  @Output() goToLoginEvent = new EventEmitter<boolean>();

  registerForm = this.formBuilder.group(
    {
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      height: [
        '',
        [Validators.required, Validators.pattern(/^-?([1-9].[1-9]\d*)?$/)],
      ],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
      acceptTerms: [false, Validators.requiredTrue],
    },
    {
      validator: MustMatch('password', 'confirmPassword'),
    }
  );
  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private route: Router,
    private userService: UserService
  ) {}

  ngOnInit() {}

  // convenience getter for easy access to form fields
  get f() {
    return this.registerForm.controls;
  }

  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }

    let newSubscriber: Register = {
      email: this.f['email'].value,
      password: this.f['password'].value,
      firstName: this.f['firstName'].value,
      lastName: this.f['lastName'].value,
      height: this.f['height'].value,
    };

    const loginSubscriber: Login = {
      email: newSubscriber.email,
      password: newSubscriber.password,
    };

    // this.accountService.register(newSubscriber).subscribe(
    //   (res) => {
    //     this.accountService.login(loginSubscriber).subscribe(
    //       (res) => {
    //         sessionStorage.setItem('AccountID', res.toString());
    //         this.parent.authorized = true;
    //         this.route.navigate(['']);
    //       },
    //       (err) => {
    //         console.log(err);
    //       }
    //     );
    //   },
    //   (err) => {
    //     console.log(err);
    //     this.showError = true;
    //   }
    // );
    this.loading = true;
    this.accountService.register(newSubscriber).then(
      (res) => {
        this.accountService.login(loginSubscriber).then(
          (res) => {
            this.userService.setAccountID(res);
            this.route.navigate(['']);
          },
          (err) => {
            console.log(err);
          }
        );
      },
      (err) => {
        console.log(err);
        this.showError = true;
        this.loading = false;
      }
    );
  }

  onReset() {
    this.submitted = false;
    this.registerForm.reset();
  }

  goToLogin() {
    this.goToLoginEvent.emit(true);
  }
}
