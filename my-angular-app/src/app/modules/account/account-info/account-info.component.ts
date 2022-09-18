import { Component, Input, OnInit } from '@angular/core';
import { Account } from 'src/app/models/Account';
import { AccountService } from 'src/app/services/Account.service';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.scss'],
})
export class AccountInfoComponent implements OnInit {
  error: string | undefined;
  account?: Account;
  loading: boolean = false;
  @Input() accountID?: number;
  @Input() private?: boolean;

  constructor(
    private accountService: AccountService,
    private userService: UserService
  ) {}

  ngOnInit(): void {
    this.loading = true;
    if (this.accountID)
      this.accountService.getAccountInfo(this.accountID).subscribe(
        (res) => {
          this.account = res;
          this.account.balance /= 100;
          this.loading = false;
        },
        (err) => {
          this.error = err.error;
          this.loading = false;
        }
      );
  }

  logout() {
    this.userService.logOut();
  }
}
