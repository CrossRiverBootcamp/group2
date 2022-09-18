import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/Login';
import { Register } from 'src/app/models/Register';
import { AccountService } from 'src/app/services/Account.service';
import { UserService } from 'src/app/services/user.service';
import { MustMatch } from '../helpers/MustMatch.validator';
import { EmailVerificationService } from 'src/app/services/emailVerification.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss'],
})
export class RegisterComponent implements OnInit {
  submitted: boolean = false;
  showError: boolean = false;
  loading: boolean = false;
  buttonVisable: boolean = true;
  success: boolean = false;
  EmailSent: boolean = false;
  verificationLoading: boolean = false;
  errorMessage: string | null = null;
  sendingEmailError: boolean = false;

  @Output() goToLoginEvent = new EventEmitter<boolean>();

  registerForm = this.formBuilder.group(
    {
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      code: ['', [Validators.required]],
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
    //private route: Router,
    private userService: UserService,
    private emailVerificationService: EmailVerificationService
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
      verificationCode: this.f['code'].value,
    };

    const loginSubscriber: Login = {
      email: newSubscriber.email,
      password: newSubscriber.password,
    };

    this.loading = true;
    this.accountService.register(newSubscriber).subscribe(
      () => {
        this.accountService.login(loginSubscriber).subscribe(
          (res) => {
            this.userService.setAccountID(res);
            //this.route.navigate(['']);
          },
          (err) => {
            console.log(err.message);
            this.errorMessage =
              'Internal server error has accured while tring to register.';
          }
        );
      },
      (err) => {
        console.log(err);
        console.log(err.message);
        this.errorMessage =
          'Internal server error has accured while tring to log into your account.';
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

  sendEmail() {
    debugger;
    this.verificationLoading = true;
    this.emailVerificationService
      .sendVerificationCode(this.f['email'].value)
      .subscribe(
        () => {
          this.EmailSent = true;
          this.verificationLoading = false;
          this.sendingEmailError = false;
        },
        (err) => {
          this.verificationLoading = false;
          this.sendingEmailError = true;
          console.error(err.message);
        }
      );
  }
}
