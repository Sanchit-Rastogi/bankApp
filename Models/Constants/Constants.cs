using System;
using BankApp.Models;

namespace BankApp.Services.Constants
{
    public class BankConstants
    {
        public static BankCharge BankCharges = new BankCharge
        {
            SameBankIMPS = 5,
            SameBankRTGS = 0,
            DifferentBankIMPS = 6,
            DifferentBankRTGS = 2,
        };

        public static string ConnectionString = "Data Source=.;Initial Catalog=BankAppDB;user=sa;password=Sanchit123@sql#;Trusted_Connection=false";

    }
}
