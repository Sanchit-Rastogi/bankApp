using System;
using System.Collections.Generic;
using BankApp.Models;
using BankApp.Services.Data;

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
                        db.SaveChanges();
                        // Update account holder balance
                        //accountHolder.Balance += amount;
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
                    db.SaveChanges();
                    // Update account holder balance
                    //accountHolder.Balance -= amount;
                }
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Transaction> DisplayTransactions()
        {
            try
            {
                List<Transaction> AccTxn = new List<Transaction>();
                // Fetch transactions for a user
                return AccTxn;
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
                    // Update source balance
                    // Update destination balance
                    //accountHolder.Balance -= amount;
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
