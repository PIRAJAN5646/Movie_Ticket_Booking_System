export interface Payment {
    PaymentId: number;
  BookingId: number;
  WalletId: number;
  Amount: number;
  Status: string;
  PaymentTime: Date;
}
