using System;
using BankApp.Models;
using BankApp.Services.Constants;
using BankApp.Services.Data;
using BankApp.Interfaces;

namespace BankApp.Services
{
    public class BankService : IBankService
    {
       
        public bool RegisterBank(string name)
        {
            try
            {
                Bank Bank = new Bank
                {
                    Name = name,
                    Id = name.AsSpan(0, 3).ToString() + DateTime.Now.ToShortDateString(),
                    BankCharges = BankConstants.BankCharges
                };

                var db = new BankDBContext();
                db.Banks.Add(Bank);
                db.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
