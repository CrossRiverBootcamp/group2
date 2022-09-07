import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';

@Component({
  selector: 'app-add-transaction',
  templateUrl: './add-transaction.component.html',
  styleUrls: ['./add-transaction.component.css']
})
export class AddTransactionComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }
   
  transactionForm: FormGroup=new FormGroup({
    accountId : new FormControl(['',Validators.required]),
    amount : new FormControl(['',Validators.required])
  })
  
  addNewTransaction(){
    
  }

}
