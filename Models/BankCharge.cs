using System;
namespace BankApp.Models
{
    public class BankCharge
    {
        public int Id { get; set; }

        public int SameBankRTGS { get; set; }

        public int SameBankIMPS { get; set; }

        public int DifferentBankRTGS { get; set; }

        public int DifferentBankIMPS { get; set; }
    }
}
