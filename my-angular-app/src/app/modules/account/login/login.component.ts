import { HttpStatusCode } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/Login';
import { AccountService } from 'src/app/services/Account.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent {
  email: string = '';
  password: string = '';
  errors: boolean = false;
  errorMessage: string = '';
  loading: boolean = false;

  @Output() goToRegisterEvent = new EventEmitter<boolean>();

  constructor(
    private accountServise: AccountService,
    //private route: Router,
    private userService: UserService
  ) {}

  Signin(): void {
    if (!this.email || !this.password) {
      this.errors = true;
      this.errorMessage = 'Email and Password are requiered.';
      return;
    }

    const login: Login = {
      email: this.email,
      password: this.password,
    };

    this.loading = true;

    this.accountServise.login(login).subscribe(
      (res) => {
        this.userService.setAccountID(res);
        //this.route.navigate(['']);
        this.loading = false;
        //location.reload();
      },
      (err) => {
        debugger;
        console.log(err);
        this.errors = true;
        this.loading = false;
        alert(err.message + err.status);
        this.errorMessage = 'Incorrect email or password.';
      }
    );
  }

  goToRegister() {
    this.goToRegisterEvent.emit(true);
  }
}
