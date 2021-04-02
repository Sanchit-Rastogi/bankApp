using System;
using System.Collections.Generic;
using BankApp.Models;

namespace BankApp.Interfaces
{
    public interface IAdminService
    {

        bool AddNewCurrency(string symbol, string name, decimal exchangeRate);

        bool RevertTransaction(string transactionId);

        List<Transaction> GetAllTransactions();

        bool SaveBankCharges(int rtgs, int imps, bool isSame);

    }
}
