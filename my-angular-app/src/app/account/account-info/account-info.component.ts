import { Component, OnInit } from '@angular/core';
import { Account } from 'src/app/models/Account';
import { AccountService } from 'src/app/services/account.service';
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
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.loading = true;
    let accountID = this.userService.getAccountID();
    if (accountID)
      this.accountService.getAccountInfo(accountID).subscribe(
        (res) => {
          this.account = res;
          this.userService.setUser(res);
          this.loading = false;
        },
        (err) => {
          console.log(err);
          this.showError = true;
        }
      );
  }

  logout() {
    this.userService.logOut();
  }
}
