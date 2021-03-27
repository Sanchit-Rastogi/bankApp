using System;
using System.IO;
using System.Xml.Serialization;
using BankApp.Models;
using BankApp.Services.Data;

namespace BankApp.Services
{
    public class BankServices
    {
        BankModel Bank = new BankModel();

        public bool RegisterBank()
        {
            Console.WriteLine("Enter your bank name");
            string name = Console.ReadLine();
            Bank.Name = name;
            Bank.DefaultCurrency = "INR";
            Bank.Id = name.AsSpan(0, 3).ToString() + DateTime.Now.ToShortDateString();
            Bank.BankCharges.SameBankRTGSCharge = 0;
            Bank.BankCharges.SameBankIMPSCharge = 5;
            Bank.BankCharges.DifferentBankRTGSCharge = 2;
            Bank.BankCharges.DifferentBankIMPSCharge = 6;
            Console.WriteLine("Bank with the name " + name + " is successfully registered !");
            SaveToXML();
            AddBankToDb();
            return true;
        }

        public void AddBankToDb()
        {
            using(var db = new BankDBContext())
            {
                db.Banks.Add(Bank);
                db.SaveChanges();
            }
        }

        public void SaveToXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BankModel));
            TextWriter writer = new StreamWriter("bankData.xml");
            serializer.Serialize(writer, Bank);
            writer.Close();
        }
    }
}
