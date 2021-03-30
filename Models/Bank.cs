using System;
using System.Collections.Generic;

namespace BankApp.Models
{
    public class Bank
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public BankCharges BankCharges { get; set; }

        public List<AccountHolder> AccountHolders { get; set; }

        public List<Employee> Employees { get; set; }

        public List<Currency> Currencies { get; set; }

        public Bank()
        {
            AccountHolders = new List<AccountHolder>();
            Employees = new List<Employee>();
            Currencies = new List<Currency>();
            BankCharges = new BankCharges();
        }
    }
}
