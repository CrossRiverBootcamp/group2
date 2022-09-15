import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmailVerification } from '../models/emailVerification';

@Injectable({
  providedIn: 'root',
})
export class EmailVerificationService {
  success: boolean = true;
  private base = 'https://localhost:7007/api/EmailVerification';
  constructor(private http: HttpClient) {}

  sendVerificationCode(emailAddress: string): Observable<void> {
    return this.http.post<void>(this.base, JSON.stringify(emailAddress), {
      headers: { 'Content-Type': 'application/json' },
    });
  }
  checkCode(emailVerification: EmailVerification): Observable<boolean> {
    return this.http.post<boolean>(`${this.base}/code`, emailVerification);
  }
}
