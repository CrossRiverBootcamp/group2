import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Transaction } from '../models/Transaction';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  private base = 'https://localhost:7007/api';

  constructor(private http: HttpClient) { }

//   addNewTransaction(newTransaction:Transaction):Observable<boolean>{
//   this.http.post<boolean>(`${this.base}/Login`,)
//   }

 }
