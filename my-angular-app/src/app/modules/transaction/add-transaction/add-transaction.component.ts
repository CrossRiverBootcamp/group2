import { Component } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { TransactionService } from 'src/app/services/transaction.service';
import { Transaction } from 'src/app/models/Transaction';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-add-transaction',
  templateUrl: './add-transaction.component.html',
  styleUrls: ['./add-transaction.component.scss'],
})
export class AddTransactionComponent {
  constructor(
    private _transactionService: TransactionService,
    private _userService: UserService
  ) {}

  error: string | undefined;
  showSuccess: boolean = false;
  loading: boolean = false;

  transactionForm: FormGroup = new FormGroup({
    accountId: new FormControl<number | null>(null, Validators.required),
    amount: new FormControl<number | null>(null, [
      Validators.required,
      Validators.min(1),
      Validators.max(1000000),
    ]),
  });

  get form() {
    return this.transactionForm.controls;
  }

  addNewTransaction() {
    if (this.setError()) return;
    this.loading = true;

    let newTransaction: Transaction = {
      fromAccount: this._userService.getAccountID() ?? -1,
      toAccount: this.form['accountId'].value,
      amount: this.form['amount'].value,
    };

    this._transactionService.addNewTransaction(newTransaction).subscribe(
      () => {
        this.showSuccess = true;
        this.loading = false;
      },
      (err) => {
        this.error = err.error;
        this.loading = false;
      }
    );
  }

  setError(): boolean {
    if (this.form['accountId']?.errors) {
      this.error = "Field 'AccountID' is required.";
      return true;
    }
    if (this.form['amount']?.errors?.['required']) {
      this.error = "Field 'Amount' is required.";
      return true;
    }
    if (this.form['amount']?.errors?.['min']) {
      this.error =
        'It is not possible to make a transfer for an amount under $1';
      return true;
    }
    if (this.form['amount']?.errors?.['max']) {
      this.error =
        'You are not authorized to make a transfer for an amount over $1,000,000';
      return true;
    }
    this.error = undefined;
    return false;
  }

  transferNext() {
    this.form['accountId'].reset();
    this.form['amount'].reset();
    this.error = undefined;
    this.showSuccess = false;
  }
}
