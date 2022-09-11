﻿using Account.Service.Models;

namespace Account.Service;

public interface IAccountService
{
    Task<int> SignInAsync(string email, string password);
    Task<AccountModel> GetAccountInfoAsync(int id);
    Task<bool> SignUpAsync(CustomerModel customerModel);
    Task ExecuteTransactionAsync(TransactionModel transactionModel);
}

