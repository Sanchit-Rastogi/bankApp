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

        public static string MainMenuOptions = "Welcome to The Bank Application Project \n \n" +
                "1. Create a Bank \n 2. Login \n 3. Exit";

        public static string AccountHolderMenuOptions = "Hi! Welcome to the Account Holder menu :- \n " +
                "1. Deposite Money. \n " +
                "2. Withdraw Money \n " +
                "3. Transfer Funds \n " +
                "4. View Transaction History \n " +
                "5. Logout";

        public static string TransferFundsMenuOptions = "Please select a tranfer option :- \n " +
                "1. Same bank RTGS transfer. \n " +
                "2. Same bank IMPS transfer. \n " +
                "3. Other bank RTGS transfer. \n " +
                "4. Other bank IMPS transfer \n " +
                "5. Go back.";

        public static string AdminMenuOptions = "Hi! Welcome to the bank staff menu :- \n" +
                " 1. Create a new account. \n " +
                " 2. Update/Delete account. \n " +
                " 3. Add new currency. \n " +
                " 4. Add service charges for this bank. \n " +
                " 5. Add service charge for other bank. \n " +
                " 6. View transactions history. \n " +
                " 7. Revert a transaction.";

        public static string RegisterMenuOptions = "Please select an account type :- \n" +
               " 1. Account holder account \n " +
               " 2. Employee account. \n " +
               " 3. Go Back";

        public enum RegisterOptionsMenu
        {
            AccountHolder = 1,
            Employee = 2
        }

        public enum AdminOptionsMenu
        {
            Register = 1,
            DeleteAccount = 2,
            NewCurrency = 3,
            SameBankService = 4,
            DiffBankService = 5,
            Transactions = 6,
            RevertTransactions = 7
        }

        public enum TransferMenuOptions
        {
            SameRTGS = 1,
            SameIMPS = 2,
            DIffRTGS = 3,
            DiffTMPS = 4
        }

        public enum AccountHolderOptions
        {
            Deposite = 1,
            Withdraw = 2,
            Transfer = 3,
            Transactions = 4,
            Logout = 5
        }

        public enum MenuOptions
        {
            RegisterBank = 1,
            Login = 2,
            Exit = 3
        }

        public enum LoginStatus
        {
            AccountHolder = 1,
            Employee = 2,
            UserNotFound = 3,
            Error = 4,
        }

    }
}
