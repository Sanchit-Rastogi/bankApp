using System;
using System.Collections.Generic;
using BankApp.Models;
using BankApp.Services.Data;
using System.Linq;

namespace BankApp.Services
{
    public class AccountServices
    {
        public bool Deposite(Transaction txn)
        {
            if (txn.Amount > 0)
            {
                try
                {
                    txn.TxnId = "TXN" + txn.SourceId + DateTime.Now.ToShortTimeString();
                    using (var db = new BankDBContext())
                    {
                        db.Transactions.Add(txn);
                        db.AccountHolders.Find(txn.SourceId).Balance += txn.Amount;
                        db.SaveChanges();
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool Withdrawal(Transaction txn)
        {
            try
            {
                txn.TxnId = "TXN" + txn.SourceId + DateTime.Now.ToShortTimeString();
                using (var db = new BankDBContext())
                {
                    db.Transactions.Add(txn);
                    db.AccountHolders.Find(txn.SourceId).Balance -= txn.Amount;
                    db.SaveChanges();
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Transaction> DisplayTransactions(int id)
        {
            try
            {
                using(var db = new BankDBContext())
                {
                    return (from txn in db.Transactions
                           where txn.SourceId == id
                           select txn).ToList();
                }
            }
            catch (Exception) { return null; }
        }

        public bool Transfer(Transaction txn)
        {
            try
            {
                txn.TxnId = "TXN" + txn.SourceId + DateTime.Now.ToShortTimeString();
                using (var db = new BankDBContext())
                {
                    db.Transactions.Add(txn);
                    db.AccountHolders.Find(txn.SourceId).Balance -= txn.Amount;
                    db.AccountHolders.Find(txn.DestinationId).Balance += txn.Amount;
                    db.SaveChanges();
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
