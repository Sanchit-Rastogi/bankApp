using System;
using BankApp.Models;
using BankApp.Services.Data;
using System.Linq;
using System.Collections.Generic;
using BankApp.Services.Constants;

namespace BankApp.Services
{
    public interface IAdminService
    {

        bool AddNewCurrency(string symbol, string name, decimal exchangeRate);

        bool RevertTransaction(string transactionId);

        List<Transaction> GetAllTransactions();

        bool SaveBankCharges(int rtgs, int imps, bool isSame);

    }

    public class AdminService : IAdminService
    {
        public bool AddNewCurrency(string symbol, string name, decimal exchangeRate)
        {
            if (exchangeRate == 0) return false;
            if (name.Length < 3 || symbol.Length != 3) return false;

            Currency currency = new Currency()
            {
                Symbol = symbol,
                Name = name,
                ExchangeRate = exchangeRate,
                IsDefault = false,
            };

            var db = new BankDBContext();
            db.Currencies.Add(currency);
            db.SaveChanges();

            return true;
        }

        public bool RevertTransaction(string transactionId)
        {
            try
            {
                var db = new BankDBContext();
                var transaction = db.Transactions.SingleOrDefault(txn => txn.TxnId == transactionId);
                if (transaction == null || transaction.IsRevereted == true) return false;

                db.Transactions.Find(transactionId).IsRevereted = true;
                if (transaction.SourceId != transaction.DestinationId) {
                    db.AccountHolders.Find(transaction.SourceId).Balance += transaction.Amount;
                    db.AccountHolders.Find(transaction.DestinationId).Balance -= transaction.Amount;
                }
                else
                    db.AccountHolders.Find(transaction.SourceId).Balance -= transaction.Amount;
                
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public List<Transaction> GetAllTransactions()
        {
            try
            {
                var db = new BankDBContext();
                return (from transaction in db.Transactions
                        select transaction).ToList();
            }
            catch (Exception) { return null; }
        }

        public bool SaveBankCharges(int rtgs, int imps, bool isSame)
        {
            try
            {
                var db = new BankDBContext();
                BankCharge bankCharge = new BankCharge()
                {
                    SameBankIMPS = isSame ? imps : BankConstants.BankCharges.SameBankIMPS,
                    SameBankRTGS = isSame ? rtgs : BankConstants.BankCharges.SameBankRTGS,
                    DifferentBankIMPS = isSame ? BankConstants.BankCharges.DifferentBankIMPS : imps,
                    DifferentBankRTGS = isSame ? BankConstants.BankCharges.DifferentBankRTGS : rtgs,
                };
                db.BankCharges.Add(bankCharge);
                db.SaveChanges();

                return true;
            }
            catch (Exception) { return false; }
        }
        
    }
}
