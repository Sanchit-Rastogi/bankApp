using System;
using System.Collections.Generic;
using BankApp.Models;
using BankApp.Services.Data;
using System.Linq;

namespace BankApp.Services
{
    public class AccountService
    {
        public Transaction CreateTransaction(int sourceId, int destinationId, decimal amount, string note)
        {
            Transaction transaction = new Transaction
            {
                TxnId = "TXN" + sourceId + DateTime.Now.ToShortTimeString(),
                TxnDate = DateTime.Now,
                SourceId = sourceId,
                DestinationId = destinationId,
                Amount = amount,
                Note = note,
                IsRevereted = false,
            };

            return transaction;
        }

        public bool Deposite(decimal amount, int sourceId)
        {
            var db = new BankDBContext();

            if (amount <= 0) return false;
        
            var user = db.AccountHolders.SingleOrDefault(user => user.Id == sourceId);
            if (user == null) return false;

            try
            { 
                Transaction txn = CreateTransaction(sourceId, sourceId, amount, "Money deposited");
                db.Transactions.Add(txn);
                db.AccountHolders.Find(sourceId).Balance += amount;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Withdrawal(decimal amount, int sourceId, string note)
        {
            var db = new BankDBContext();

            if (db.AccountHolders.Find(sourceId).Balance < amount) return false;

            var user = db.AccountHolders.SingleOrDefault(user => user.Id == sourceId);
            if (user == null) return false;

            try
            { 
                Transaction transaction = CreateTransaction(sourceId, sourceId, -amount, note);
                db.Transactions.Add(transaction);
                db.AccountHolders.Find(sourceId).Balance -= amount;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public List<Transaction> DisplayTransactions(int id)
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
                db.AccountHolders.Find(sourceId).Balance -= (amount+txnCharge);
                db.AccountHolders.Find(destinationId).Balance += amount;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool OtherBankTransfer(int sourceId, int destinationId, decimal amount, string note, int charge, string destinationBankId)
        {
            var db = new BankDBContext();

            if (db.AccountHolders.Find(sourceId).Balance < amount) return false;

            var user = db.AccountHolders.SingleOrDefault(user => user.Id == sourceId);
            if (user == null) return false;

            try
            {
                decimal txnCharge = (amount * charge) / 100;
                Transaction txn = CreateTransaction(sourceId, destinationId, amount, note);
                db.Transactions.Add(txn);
                db.AccountHolders.Find(sourceId).Balance -= (amount + txnCharge);
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
