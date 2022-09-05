import { Component, OnInit } from '@angular/core';
import { Account } from 'src/app/models/Account';
import { AccountService } from 'src/app/services/Account.service';

@Component({
  selector: 'app-account-info',
  templateUrl: './account-info.component.html',
  styleUrls: ['./account-info.component.scss'],
})
export class AccountInfoComponent implements OnInit {
  AccountID?: number = Number(sessionStorage.getItem('AccountID'));
  showError: boolean = false;
  account?: Account;
  loading: boolean = false;
  constructor(private accountService: AccountService) {}

  ngOnInit(): void {
    this.loading = true;
    if (this.AccountID)
      this.accountService.getAccountInfo(this.AccountID).then(
        (res) => {
          this.account = res;
          this.loading = false;
        },
        (err) => {
          console.log(err);
          this.showError = true;
        }
      );
  }
}
