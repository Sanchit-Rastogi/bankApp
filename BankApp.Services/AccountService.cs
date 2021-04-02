using System;
using System.Collections.Generic;
using BankApp.Models;
using BankApp.Services.Data;
using System.Linq;
using BankApp.Interfaces;

namespace BankApp.Services
{

    public class AccountService : IAccountService
    {
        private Transaction CreateTransaction(int sourceAccountNumber, int destinationAccountNumber, decimal amount, string note)
        {
            Transaction transaction = new Transaction
            {
                TxnId = "TXN" + sourceAccountNumber + DateTime.Now.ToShortTimeString(),
                TxnDate = DateTime.Now,
                SourceId = sourceAccountNumber,
                DestinationId = destinationAccountNumber,
                Amount = amount,
                Note = note,
            };

            return transaction;
        }

        public bool Deposite(decimal amount, int sourceAccountNumber)
        {
            var db = new BankDBContext();

            if (amount <= 0) return false;
        
            var user = db.AccountHolders.SingleOrDefault(user => user.Id == sourceAccountNumber);
            if (user == null) return false;

            try
            { 
                Transaction txn = CreateTransaction(sourceAccountNumber, sourceAccountNumber, amount, "Money deposited");
                db.Transactions.Add(txn);
                user.Balance += amount;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Withdrawal(decimal amount, int sourceAccountNumber, string note)
        {
            var db = new BankDBContext();

            if (db.AccountHolders.Find(sourceAccountNumber).Balance < amount) return false;

            var user = db.AccountHolders.SingleOrDefault(user => user.Id == sourceAccountNumber);
            if (user == null) return false;

            try
            { 
                Transaction transaction = CreateTransaction(sourceAccountNumber, sourceAccountNumber, -amount, note);
                db.Transactions.Add(transaction);
                user.Balance -= amount;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public List<Transaction> GetTransactions(int id)
        {
            var db = new BankDBContext();
            var user = db.AccountHolders.SingleOrDefault(user => user.Id == id);
            if (user == null) return null;

            try
            {
                return (from txn in db.Transactions
                        where txn.SourceId == id || txn.DestinationId == id
                        select txn).ToList();
            }
            catch (Exception) { return null; }
        }

        public bool Transfer(int sourceId, int destinationId, decimal amount, string note, int charge)
        {
            var db = new BankDBContext();

            if(db.AccountHolders.Find(sourceId).Balance < amount) return false;

            var user = db.AccountHolders.SingleOrDefault(user => user.Id == sourceId);
            if (user == null) return false;

            try
            {
                decimal txnCharge = (amount * charge) / 100;
                Transaction txn = CreateTransaction(sourceId, destinationId, amount, note);
                db.Transactions.Add(txn);
                user.Balance -= (amount+txnCharge);
                db.AccountHolders.Find(destinationId).Balance += amount;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
