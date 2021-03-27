using System;
using BankApp.Models;

namespace BankApp.Services.Constants
{
    public class BankConstants
    {
        public static BankCharges BankCharges = new BankCharges
        {
            SameBankIMPSCharge = 0,
            SameBankRTGSCharge = 2,
            DifferentBankIMPSCharge = 2,
            DifferentBankRTGSCharge = 3,
        };
    }
}
