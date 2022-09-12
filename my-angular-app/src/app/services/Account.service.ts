import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Register } from '../models/Register';
import { Login } from '../models/Login';
import { Account } from '../models/Account';

@Injectable({
  providedIn: 'root',
})
export class AccountService {
  private base = 'https://localhost:7007/api';

  constructor(private http: HttpClient) {}

  register(newUser: Register): Observable<boolean> {
    return this.http.post<boolean>(`${this.base}/SignUp`, newUser);
  }

  login(login: Login): Observable<number> {
    return this.http.post<number>(`${this.base}/Login`, login);
  }

  getAccountInfo(accountID: number): Observable<Account> {
    return this.http.get<Account>(`${this.base}/Account/${accountID}`);
  }
}
