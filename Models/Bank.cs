using System;
using System.Collections.Generic;

namespace BankApp.Models
{
    public class BankModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string DefaultCurrency { get; set; }

        public BankCharges BankCharges { get; set; }

        public List<AccountHolder> AccountHolders { get; set; }

        public List<Employee> Employees { get; set; }

        public List<Currency> Currencies { get; set; }

        public BankModel()
        {
            AccountHolders = new List<AccountHolder>();
            Employees = new List<Employee>();
            Currencies = new List<Currency>();
            BankCharges = new BankCharges();
        }
    }
}
