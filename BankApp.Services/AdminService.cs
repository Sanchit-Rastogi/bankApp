using System;
using BankApp.Models;
using BankApp.Services.Data;
using System.Linq;
using System.Collections.Generic;
using BankApp.Services.Constants;

namespace BankApp.Services
{
    public class AdminService
    {
        public bool AddNewCurrency(string sym, string name, decimal xchRate)
        {
            if (xchRate == 0) return false;
            if (name.Length < 3 || sym.Length != 3) return false;

            Currency currency = new Currency()
            {
                Symbol = sym,
                Name = name,
                ExchangeRate = xchRate,
                IsDefault = false,
            };

            using var db = new BankDBContext();
            db.Currencies.Add(currency);
            db.SaveChanges();

            return true;
        }

        public bool DeleteAccount(string accId)
        {
            try
            {
                using var db = new BankDBContext();
                db.AccountHolders.Remove(db.AccountHolders.SingleOrDefault(acc => acc.AccId == accId));
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RevertTransaction(string txnId)
        {
            try
            {
                using var db = new BankDBContext();
                db.Transactions.Remove(db.Transactions.SingleOrDefault(txn => txn.TxnId == txnId));
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public List<Transaction> DisplayTransactions()
        {
            try
            {
                using var db = new BankDBContext();
                return (from txn in db.Transactions
                        select txn).ToList();
            }
            catch (Exception) { return null; }
        }

        public bool EditSameBankCharges(int rtgs, int imps)
        {
            try
            {
                using var db = new BankDBContext();
                BankCharge bankCharge = new BankCharge()
                {
                    SameBankIMPSCharge = imps,
                    SameBankRTGSCharge = rtgs,
                    DifferentBankIMPSCharge = BankConstants.BankCharges.DifferentBankIMPSCharge,
                    DifferentBankRTGSCharge = BankConstants.BankCharges.DifferentBankRTGSCharge,
                };
                db.BankCharges.Add(bankCharge);
                db.SaveChanges();

                return true;
            }
            catch (Exception) { return false; }
        }

        public bool EditDiffBankCharges(int rtgs, int imps)
        {
            try
            {
                using var db = new BankDBContext();
                BankCharge bankCharge = new BankCharge()
                {
                    SameBankIMPSCharge = BankConstants.BankCharges.SameBankIMPSCharge,
                    SameBankRTGSCharge = BankConstants.BankCharges.SameBankRTGSCharge,
                    DifferentBankIMPSCharge = imps,
                    DifferentBankRTGSCharge = rtgs,
                };
                db.BankCharges.Add(bankCharge);
                db.SaveChanges();

                return true;
            }
            catch (Exception) { return false; }
        }

        public bool RegisterUser(string role, User inputUser)
        {
            if (role == "AH")
            {
                AccountHolder accountHolder = new AccountHolder();
                accountHolder.User.Name = inputUser.Name;
                accountHolder.User.Password = inputUser.Password;
                accountHolder.User.Username = inputUser.Username;
                accountHolder.AccId = "AH" + inputUser.Name[..3] + DateTime.Now.ToLongDateString();
                try
                {
                    using var db = new BankDBContext();
                    var accHolder = from acc in db.AccountHolders
                                    where acc.User.Name == inputUser.Name && acc.User.Password == inputUser.Password
                                    select acc;
                    if (accHolder != null) return true;
                    else
                    {
                        db.AccountHolders.Add(accountHolder);
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                Employee loginEmployee = new Employee();
                loginEmployee.User.Name = inputUser.Name;
                loginEmployee.User.Username = inputUser.Username;
                loginEmployee.User.Password = inputUser.Password;
                loginEmployee.EmployeeId = "EMP" + inputUser.Name[..3] + DateTime.Now.ToLongDateString();

                try
                {
                    using var db = new BankDBContext();
                    var employee = from emp in db.Employees
                                   where emp.User.Name == inputUser.Name && emp.User.Password == inputUser.Password
                                   select emp;
                    if (employee != null) return true;
                    else
                    {
                        db.Employees.Add(loginEmployee);
                        db.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
