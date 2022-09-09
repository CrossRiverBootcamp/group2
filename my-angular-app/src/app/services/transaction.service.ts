import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Transaction } from '../models/Transaction';

@Injectable({
  providedIn: 'root',
})
export class TransactionService {
  private base = 'https://localhost:7265/api/transaction';

  constructor(private http: HttpClient) {}

  addNewTransaction(newTransaction: Transaction): Observable<boolean> {
    return this.http.post<boolean>(`${this.base}`, newTransaction);
  }
}
