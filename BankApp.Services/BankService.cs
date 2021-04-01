using System;
using BankApp.Models;
using BankApp.Services.Constants;
using BankApp.Services.Data;

namespace BankApp.Services
{
    public interface IBankService
    {
        bool RegisterBank(string name);
    }

    public class BankService : IBankService
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
