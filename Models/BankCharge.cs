using System;
namespace BankApp.Models
{
    public class BankCharge
    {
        public int Id { get; set; }

        public int SameBankRTGSCharge { get; set; }

        public int SameBankIMPSCharge { get; set; }

        public int DifferentBankRTGSCharge { get; set; }

        public int DifferentBankIMPSCharge { get; set; }
    }
}
