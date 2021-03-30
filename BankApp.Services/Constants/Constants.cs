using System;
using BankApp.Models;

namespace BankApp.Services.Constants
{
    public class BankConstants
    {
        public static BankCharges BankCharges = new BankCharges
        {
            SameBankIMPSCharge = 5,
            SameBankRTGSCharge = 0,
            DifferentBankIMPSCharge = 6,
            DifferentBankRTGSCharge = 2,
        };
    }
}
