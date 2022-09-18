import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private accountID?: number;
  private authoBehavior = new BehaviorSubject<boolean>(false);
  private auth = this.authoBehavior.asObservable();

  getAuth(): Observable<boolean> {
    if (this.accountID) this.setAuth(true);
    else this.setAuth(false);
    return this.auth;
  }

  setAuth(bool: boolean) {
    this.authoBehavior.next(bool);
  }

  setAccountID(id: number) {
    this.setAuth(true);
    this.accountID = id;
  }

  getAccountID(): number | undefined {
    return this.accountID;
  }

  logOut() {
    this.accountID = undefined;
    this.setAuth(false);
  }
}
