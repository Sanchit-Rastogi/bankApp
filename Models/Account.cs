using System;
using System.Collections.Generic;

namespace BankApp.Models
{
    public class AccountHolder : User
    {
        public string AccId { get; set; }

        public decimal Balance { get; set; }

        public List<Transaction> Transactions;

        public AccountHolder()
        {
            Transactions = new List<Transaction>();
        }

    }
}