using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using BankApp.Models;

namespace BankApp.Services
{
    public class AccountServices
    {
        BankModel BankData = new BankModel();
        public String AccId;

        public void LoadData()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BankModel));
            FileStream fs = new FileStream("bankData.xml", FileMode.Open);
            BankData = (BankModel)serializer.Deserialize(fs);
        }


        public bool Deposite(decimal amount, string note, DateTime date)
        {
            LoadData();
            if (amount > 0)
            {
                try
                {
                    AccountHolder accountHolder = BankData.AccountHolders.Find(_ => _.AccId == AccId);
                    string id = "TXN" + AccId + DateTime.Now.ToShortTimeString();
                    var deposite = new Transaction
                    {
                        Amount = amount,
                        Note = note,
                        Date = date,
                        TxnId = id,
                    };
                    accountHolder.Transactions.Add(deposite);
                    accountHolder.Balance += amount;
                    SaveToXML();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool Withdrawal(decimal amount, string note, DateTime date)
        {
            LoadData();
            try
            {
                AccountHolder accountHolder = BankData.AccountHolders.Find(_ => _.AccId == AccId);
                if (accountHolder.Balance - amount > 0)
                {
                    string id = "TXN" + AccId + DateTime.Now.ToShortTimeString();
                    var withdrawal = new Transaction
                    {
                        Amount = -amount,
                        Note = note,
                        Date = date,
                        TxnId = id,
                    };
                    accountHolder.Transactions.Add(withdrawal);
                    accountHolder.Balance -= amount;
                    SaveToXML();
                    return true;
                }
                return false;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Transaction> DisplayTransactions()
        {
            LoadData();
            try
            {
                AccountHolder accountHolder = BankData.AccountHolders.Find(_ => _.AccId == AccId);
                return accountHolder.Transactions;
            }
            catch (Exception) { return null; }

        }

        public void SaveToXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BankModel));
            TextWriter writer = new StreamWriter("bankData.xml");
            serializer.Serialize(writer, BankData);
            writer.Close();
        }
    }
}
