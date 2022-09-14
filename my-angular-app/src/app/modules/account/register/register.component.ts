import { Component, EventEmitter, OnInit, Output, ViewChild } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/Login';
import { Register } from 'src/app/models/Register';
import { AccountService } from 'src/app/services/Account.service';
import { UserService } from 'src/app/services/user.service';
import { MustMatch } from '../helpers/MustMatch.validator';
import { MatTooltip } from '@angular/material/tooltip';
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
  buttonVisable:boolean = true;
  success: boolean = false;
  @Output() goToLoginEvent = new EventEmitter<boolean>();

  registerForm = this.formBuilder.group(
    {
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
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
    private userService: UserService,
    private emailVerificationService : EmailVerificationService
  ) {
    debugger;
   

  }

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
    };

    const loginSubscriber: Login = {
      email: newSubscriber.email,
      password: newSubscriber.password,
    };

    this.loading = true;
    this.accountService.register(newSubscriber).subscribe(
      (res) => {
        this.accountService.login(loginSubscriber).subscribe(
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

  sendEmail(){
    this.emailVerificationService.sendVerificationCode(this.f['email'].value)
    .subscribe(
      (res:any) => {
        this.buttonVisable = false;
      },
      (errL:any)=>{

      })
  }
  checkCode(event:any){
    this.emailVerificationService.checkCode({"email":this.f['email'].value,"code":event.target.value})
          .subscribe(
            (res:any) => {
              this.success = true;
            },
            (errL:any)=>{
      
            }
          )
  }
  
}
