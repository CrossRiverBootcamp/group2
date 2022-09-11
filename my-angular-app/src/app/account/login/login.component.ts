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
  loading: boolean = false;

  @Output() goToRegisterEvent = new EventEmitter<boolean>();

  constructor(
    private accountServise: AccountService,
    private route: Router,
    private userService: UserService
  ) {}

  Signin(): void {
    if (!this.email || !this.password) {
      this.errors = true;
      return;
    }

    const login: Login = {
      email: this.email,
      password: this.password,
    };

    this.loading = true;

    this.accountServise.login(login).subscribe(
      (res) => {
        debugger;
        this.userService.setAccountID(res);
        this.route.navigate(['']);
        this.loading = false;
      },
      (err) => {
        console.log(err);
        this.errors = true;
        this.loading = false;
      }
    );
  }

  goToRegister() {
    this.goToRegisterEvent.emit(true);
  }
}
