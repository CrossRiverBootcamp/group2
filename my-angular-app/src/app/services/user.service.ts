import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Login } from '../models/Login';
import { AccountService } from './Account.service';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  private static accountID?: number = Number(
    sessionStorage.getItem('accountID')
  );
  private static authoBehavior = new BehaviorSubject<boolean>(false);
  private static auth = UserService.authoBehavior.asObservable();

  // constructor(private accountServise: AccountService) {
  //   let login: Login = { email: 'string@string.com', password: '123123' };
  //   this.accountServise.login(login).subscribe(
  //     (res) => {
  //       this.setAccountID(res);
  //     },
  //     (err) => {
  //       console.log(err);
  //     }
  //   );
  // }

  getAuth(): Observable<boolean> {
    if (UserService.accountID) this.setAuth(true);
    else this.setAuth(false);
    return UserService.auth;
  }

  setAuth(bool: boolean) {
    UserService.authoBehavior.next(bool);
  }

  setAccountID(id: number) {
    this.setAuth(true);
    sessionStorage.setItem('accountID', id.toString());
  }

  getAccountID(): number | undefined {
    return UserService.accountID;
  }

  logOut() {
    sessionStorage.clear();
    this.setAuth(false);
  }
}
