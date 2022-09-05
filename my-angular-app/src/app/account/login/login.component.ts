import { Component, Inject, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { Login } from 'src/app/models/Login';
import { AccountService } from 'src/app/services/Account.service';

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

  constructor(
    private accountServise: AccountService,
    private route: Router,
    @Inject(AppComponent) private parent: AppComponent
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
    // this.accountServise.login(login).subscribe(
    //   (res) => {
    //     sessionStorage.setItem('AccountID', res.toString());
    //     this.parent.authorized = true;
    //     this.route.navigate(['']);
    //   },
    //   (err) => {
    //     console.log(err);
    //   }
    // );
    this.accountServise.login(login).then(
      (res) => {
        sessionStorage.setItem('AccountID', res.toString());
        this.parent.authorized = true;
        this.route.navigate(['']);
        this.loading = false;
      },
      (err) => {
        console.log(err);
        this.loading = false;
      }
    );
  }

  goToRegister() {
    this.parent.register = true;
  }
}
