import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { TransactionService } from 'src/app/services/transaction.service';
import { Transaction } from 'src/app/models/Transaction';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'app-add-transaction',
  templateUrl: './add-transaction.component.html',
  styleUrls: ['./add-transaction.component.css']
})
export class AddTransactionComponent implements OnInit {

  constructor(private _transactionService:TransactionService, private _userService:UserService) { }

  ngOnInit(): void {
  }
  newTransaction!:Transaction; 
  transactionForm: FormGroup=new FormGroup({
    accountId : new FormControl(['',[Validators.required,Validators.min(1)]]),
    amount : new FormControl(['',[Validators.required, Validators.min(1),Validators.min(1000000)] ])
  })
  
  addNewTransaction(){
    this.newTransaction = this.transactionForm.value;
    this.newTransaction.fromAccount!=this._userService.getAccountID();
    this._transactionService.addNewTransaction(this.newTransaction).subscribe(alert("transactionAdded"))
  }

}
