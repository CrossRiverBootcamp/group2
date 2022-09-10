import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Account } from 'src/app/models/Account';
import { AccountService } from 'src/app/services/Account.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.scss'],
})
export class AccountInfoComponent implements OnInit {
  showError: boolean = false;
  account?: Account;
  loading: boolean = false;

  constructor(
    private accountService: AccountService,
    private userService: UserService,
    private route: Router
  ) {}

  ngOnInit(): void {
    this.loading = true;
    let accountID = this.userService.getAccountID();
    if (accountID)
      this.accountService.getAccountInfo(accountID).subscribe(
        (res) => {
          this.account = res;
          this.loading = false;
        },
        (err) => {
          console.log(err);
          this.showError = true;
          this.loading = false;
        }
      );
  }

  logout() {
    this.userService.logOut();
    location.reload();
    this.route.navigate(['']);
  }
}
