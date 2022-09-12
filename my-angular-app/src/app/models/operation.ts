export interface Operation {
  secondSideAccountId: number;
  credit: boolean;
  transactionAmount: number;
  balance: number;
  operationTime: Date;
}
