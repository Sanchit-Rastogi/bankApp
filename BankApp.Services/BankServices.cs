using System;
using BankApp.Models;
using BankApp.Services.Constants;
using BankApp.Services.Data;

namespace BankApp.Services
{
    public class BankServices
    {
        BankModel Bank = new BankModel();

        public bool RegisterBank(string name)
        {
            Bank.Name = name;
            Bank.DefaultCurrency = "INR";
            Bank.Id = name.AsSpan(0, 3).ToString() + DateTime.Now.ToShortDateString();
            Bank.BankCharges = BankConstants.BankCharges;
            using (var db = new BankDBContext())
            {
                db.Banks.Add(Bank);
                db.SaveChanges();
            }
            return true;
        }
    }
}
