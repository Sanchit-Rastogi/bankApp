using System;
using BankApp.Models;

namespace BankApp.Services.Constants
{
    public class BankConstants
    {
        public static BankCharge BankCharges = new BankCharge
        {
            SameBankIMPSCharge = 5,
            SameBankRTGSCharge = 0,
            DifferentBankIMPSCharge = 6,
            DifferentBankRTGSCharge = 2,
        };
    }
}
