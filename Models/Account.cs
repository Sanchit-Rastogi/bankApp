using System;
using System.Collections.Generic;

namespace BankApp.Models
{
    public class AccountHolder
    {
        public User User { get; set; }

        public string AccId { get; set; }

        public decimal Balance { get; set; }

    }
}