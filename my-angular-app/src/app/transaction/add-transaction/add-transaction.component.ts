import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { TransactionService } from 'src/app/services/transaction.service';

@Component({
  selector: 'app-add-transaction',
  templateUrl: './add-transaction.component.html',
  styleUrls: ['./add-transaction.component.css']
})
export class AddTransactionComponent implements OnInit {

  constructor(private _transactionService:TransactionService) { }

  ngOnInit(): void {
  }
   
  transactionForm: FormGroup=new FormGroup({
    accountId : new FormControl(['',[Validators.required,Validators.min(1)]]),
    amount : new FormControl(['',[Validators.required, Validators.min(1),Validators.min(1000000)] ])
  })
  
  addNewTransaction(){
     this._transactionService.addNewTransaction(this.transactionForm.value).subscribe(alert("transactionAdded"))
  }

}
