using System;
using BankApp.Models;
using BankApp.Services.Constants;
using BankApp.Services.Data;

namespace BankApp.Services
{
    public class BankService
    {
       
        public bool RegisterBank(string name)
        {
            Bank Bank = new Bank
            {
                Name = name,
                Id = name.AsSpan(0, 3).ToString() + DateTime.Now.ToShortDateString(),
                BankCharges = BankConstants.BankCharges
            };
            using (var db = new BankDBContext())
            {
                db.Banks.Add(Bank);
                db.SaveChanges();
            }
            return true;
        }
    }
}
