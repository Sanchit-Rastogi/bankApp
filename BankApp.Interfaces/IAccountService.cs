using System;
using System.Collections.Generic;
using BankApp.Models;

namespace BankApp.Interfaces
{
    public interface IAccountService
    {
        bool Deposite(decimal amount, int sourceAccountNumber);

        bool Withdrawal(decimal amount, int sourceAccountNumber, string note);

        List<Transaction> GetTransactions(int id);

        bool Transfer(int sourceId, int destinationId, decimal amount, string note, int charge);
    }
}
