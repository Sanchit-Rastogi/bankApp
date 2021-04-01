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

        public static string ConnectionString = "Data Source=.;Initial Catalog=BankAppDB;user=sa;password=Sanchit123@sql#;Trusted_Connection=false";

    }
}
