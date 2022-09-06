import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Account } from '../models/Account';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private user?: Account;
  private authorized: boolean = false;
  private accountID?: number;
  private authoBehavior = new BehaviorSubject<boolean>(false);
  private auth = this.authoBehavior.asObservable();
  constructor() {}

  getAuth(): Observable<boolean> {
    return this.auth;
  }

  setAuth(bool: boolean) {
    this.authoBehavior.next(bool);
  }

  setUser(user: Account) {
    this.user = user;
    this.authorized = true;
    this.setAuth(true);
  }

  getUser() {
    return this.user;
  }

  setAccountID(id: number) {
    this.authorized = true;
    this.setAuth(true);
    this.accountID = id;
  }

  getAccountID(): number | undefined {
    return this.accountID;
  }

  isAuthorized(): boolean {
    return this.authorized;
  }

  logOut() {
    this.user = undefined;
    this.authorized = false;
    this.accountID = undefined;
    this.setAuth(false);
  }
}
