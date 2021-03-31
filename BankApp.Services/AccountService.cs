using System;
using System.Collections.Generic;
using BankApp.Models;
using BankApp.Services.Data;
using System.Linq;

namespace BankApp.Services
{
    public class AccountService
    {
        public Transaction CreateTransaction(int srcId, int dstId, decimal amt, string note)
        {
            Transaction txn = new Transaction
            {
                TxnId = "TXN" + srcId + DateTime.Now.ToShortTimeString(),
                TxnDate = DateTime.Now,
                SourceId = srcId,
                DestinationId = dstId,
                Amount = amt,
                Note = note
            };

            return txn;
        }

        public bool Deposite(decimal amount, int srcId)
        {
            if (amount < 0) return false;

            try
            {
                Transaction txn = CreateTransaction(srcId, srcId, amount, "Money deposited");
                using var db = new BankDBContext();
                db.Transactions.Add(txn);
                db.AccountHolders.Find(txn.SourceId).Balance += txn.Amount;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool Withdrawal(decimal amount, int srcId, string note)
        {
            using var db = new BankDBContext();

            if (db.AccountHolders.Find(srcId).Balance < amount) return false;

            try
            {
                Transaction txn = CreateTransaction(srcId, srcId, -amount, note);
                db.Transactions.Add(txn);
                db.AccountHolders.Find(txn.SourceId).Balance -= txn.Amount;
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
            try
            {
                using var db = new BankDBContext();
                return (from txn in db.Transactions
                        where txn.SourceId == id
                        select txn).ToList();
            }
            catch (Exception) { return null; }
        }

        public bool Transfer(int srcId, int dstId, decimal amt, string note, int charge)
        {
            using var db = new BankDBContext();

            if(db.AccountHolders.Find(srcId).Balance < amt) return false;

            try
            {
                decimal txnCharge = (amt * charge) / 100;
                Transaction txn = CreateTransaction(srcId, dstId, amt, note);
                db.Transactions.Add(txn);
                db.AccountHolders.Find(txn.SourceId).Balance -= (txn.Amount+txnCharge);
                db.AccountHolders.Find(txn.DestinationId).Balance += txn.Amount;
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public bool OtherBankTransfer(int srcId, int dstId, decimal amt, string note, int charge, string dstBankId)
        {
            using var db = new BankDBContext();

            if (db.AccountHolders.Find(srcId).Balance < amt) return false;

            try
            {
                decimal txnCharge = (amt * charge) / 100;
                Transaction txn = CreateTransaction(srcId, dstId, amt, note);
                db.Transactions.Add(txn);
                db.AccountHolders.Find(txn.SourceId).Balance -= (txn.Amount + txnCharge);
                db.AccountHolders.Find(txn.DestinationId).Balance += txn.Amount;
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
