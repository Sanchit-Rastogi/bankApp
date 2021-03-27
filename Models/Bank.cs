using System;
using System.Collections.Generic;

namespace BankApp.Models
{
    public class BankModel
    {
        public string Name { get; set; }

        public string Id { get; set; }

        public string DefaultCurrency { get; set; }

        public Charges BankCharges { get; set; }

        public List<AccountHolder> AccountHolders;

        public List<Employee> Employees;

        public List<Currency> Currencies;

        public BankModel()
        {
            AccountHolders = new List<AccountHolder>();
            Employees = new List<Employee>();
            Currencies = new List<Currency>();
            BankCharges = new Charges();
        }
    }
}
