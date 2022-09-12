export interface Operation {
  isCredit: boolean;
  secondSideId: number;
  amount: number;
  balance: number;
  date: Date;
}
