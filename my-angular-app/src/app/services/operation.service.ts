import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Operation } from '../models/operation';

@Injectable({
  providedIn: 'root',
})
export class OperationService {
  private base = 'https://localhost:7007/api/Operation';
  DATA: Operation[] = [];
  //   {
  //     isCredit: true,
  //     secondSideId: 1,
  //     amount: 12,
  //     balance: 710,
  //     date: new Date(),
  //   },
  //   {
  //     isCredit: false,
  //     secondSideId: 4,
  //     amount: 270,
  //     balance: 659,
  //     date: new Date(10),
  //   },
  //   {
  //     isCredit: true,
  //     secondSideId: 9,
  //     amount: 55,
  //     balance: 435,
  //     date: new Date(11),
  //   },
  //   {
  //     isCredit: true,
  //     secondSideId: 7,
  //     amount: 70,
  //     balance: 890,
  //     date: new Date(70),
  //   },
  //   {
  //     isCredit: true,
  //     secondSideId: 1,
  //     amount: 12,
  //     balance: 710,
  //     date: new Date(),
  //   },
  //   {
  //     isCredit: false,
  //     secondSideId: 4,
  //     amount: 270,
  //     balance: 659,
  //     date: new Date(10),
  //   },
  //   {
  //     isCredit: true,
  //     secondSideId: 5,
  //     amount: 55,
  //     balance: 435,
  //     date: new Date(11),
  //   },
  //   {
  //     isCredit: false,
  //     secondSideId: 7,
  //     amount: 70,
  //     balance: 890,
  //     date: new Date(70),
  //   },
  // ];

  constructor(private http: HttpClient) {}

  getOperations(
    accountID: number,
    currentPage: number,
    pageSize: number
  ): Observable<Operation[]> {
    return this.http.get<Operation[]>(
      `${this.base}/${accountID}?currentPage=${currentPage}&pageSize=${pageSize}`
    );
  }

  getOperationsDemo(): Promise<Operation[]> {
    return new Promise((resolve) => {
      setTimeout(() => {
        resolve(this.DATA);
      }, 1000);
    });
  }
}
