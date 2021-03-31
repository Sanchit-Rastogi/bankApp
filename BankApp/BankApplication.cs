using System;
using BankApp.Services;
using BankApp.Models;
using System.Collections.Generic;
using BankApp.Services.Utilities;
using BankApp.Services.Constants;

namespace BankApplication
{
    public class BankApplication
    {
        Utility Utility;
        BankService BankServices;
        User User;
        UserService UserServices;
        AccountService AccountServices;
        AdminService AdminServices;

        public BankApplication()
        {
            Initialize();
        }

        public void Initialize()
        {
            this.Utility = new Utility();
            this.BankServices = new BankService();
            this.User = new User();
            this.UserServices = new UserService();
            this.AccountServices = new AccountService();
            this.AdminServices = new AdminService();
            DisplayMainMenu();
        }

        public void DisplayMainMenu()
        {
            int option = this.Utility.GetIntegerInput("Welcome to The Bank Application Project \n \n" +
                " Please enter option from the list :- \n " +
                "1. Create a Bank \n 2. Login \n 3. Exit");
            switch (option)
            {
                case 1:
                    string name = this.Utility.GetStringInput("Registereing a new bank \n Enter your bank name");
                    bool isRegistered = this.BankServices.RegisterBank(name);
                    if (isRegistered) {
                        Console.WriteLine("New Bank Successfully Created.");
                        DisplayMainMenu();
                    }
                    break;
                case 2:
                    this.User = this.GetUserDetails();
                    string loggedInStatus = this.UserServices.LoginUser(this.User);
                    if (loggedInStatus == "AccountHolder") AccountHolderMenu();
                    else if (loggedInStatus == "Employee") AdminMenu();
                    else Console.WriteLine("Login error ! Please try again.");
                    break;
                case 3:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Please select a valid option !!");
                    DisplayMainMenu();
                    break;
            }
        }

        public User GetUserDetails()
        {
            this.User.Name = this.Utility.GetStringInput("Enter your name");
            this.User.Username = this.Utility.GetStringInput("Enter your username");
            this.User.Password = this.Utility.GetStringInput("Enter your password");
            return this.User;
        }

        public void AccountHolderMenu()
        {
            Console.Clear();
            int option = this.Utility.GetIntegerInput("Hi! Welcome to the Account Holder menu :- \n " +
                "1. Deposite Money. \n " +
                "2. Withdraw Money \n " +
                "3. Transfer Funds \n " +
                "4. View Transaction History \n " +
                "5. Go back to main menu");
            switch (option)
            {
                case 1:
                    decimal depositeAmount = this.Utility.GetDecimalInput("Enter amount to be deposited :- ");
                    bool isDeposited = this.AccountServices.Deposite(depositeAmount, this.User.Id);
                    if (isDeposited) Console.WriteLine("Amount Successfully deposited");
                    else Console.WriteLine("Unable to deposite amount, Try again later !");

                    this.AccountHolderMenu();
                    break;
                case 2:
                    decimal withdrawalAmount = this.Utility.GetDecimalInput("Enter amount to be withdrawn :- ");
                    string note = this.Utility.GetStringInput("Enter note for withdrawl :- ");
                    bool isWithdrawn = this.AccountServices.Withdrawal(withdrawalAmount, this.User.Id, note);
                    if (isWithdrawn) Console.WriteLine("Amount Successfully withdrawn");
                    else Console.WriteLine("Unable to withdraw amount, Try again later !");

                    this.AccountHolderMenu();
                    break;
                case 3:
                    this.TransferFundsMenu();
                    break;
                case 4:
                    List<Transaction> transactions = this.AccountServices.DisplayTransactions(this.User.Id);
                    bool isSuccessfull = DisplayTransactions(transactions);
                    if (isSuccessfull)
                        AccountHolderMenu();
                    break;
                case 5:
                    DisplayMainMenu();
                    break;
                default:
                    Console.WriteLine("Please select a valid option !!");
                    AccountHolderMenu();
                    break;
            }
        }

        public bool DisplayTransactions(List<Transaction> transactions)
        {
            Console.WriteLine("TXN ID \t\t NOTE \t\t AMOUNT \t\t DATE");
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"{transaction.TxnId} \t\t {transaction.Note} \t\t {transaction.Amount} \t\t {transaction.TxnDate}");
            }
            Console.WriteLine("\n Enter something to go back");
            String result = Console.ReadLine();

            if (result != null) return true;
            else return false;
        }

        public void TransferFundsMenu()
        {
            Console.Clear();
            int option = this.Utility.GetIntegerInput("Please select a tranfer option :- \n " +
                "1. Same bank RTGS transfer. \n " +
                "2. Same bank IMPS transfer. \n " +
                "3. Other bank RTGS transfer. \n " +
                "4. Other bank IMPS transfer \n " +
                "5. Go back.");
            int dstId = this.Utility.GetIntegerInput("Enter reciver account Id.");
            decimal amt = this.Utility.GetDecimalInput("Enter amount to be tranfered.");
            string note = this.Utility.GetStringInput("Enter transfer note.");
            bool isTransfered = false;
            switch (option)
            {
                case 1:
                    isTransfered = this.AccountServices.Transfer(this.User.Id, dstId, amt, note, BankConstants.BankCharges.SameBankRTGSCharge);
                    break;
                case 2:
                    isTransfered = this.AccountServices.Transfer(this.User.Id, dstId, amt, note, BankConstants.BankCharges.SameBankIMPSCharge);
                    break;
                case 3:
                    string dstBankId = this.Utility.GetStringInput("Enter destination account bank Id.");
                    isTransfered = this.AccountServices.OtherBankTransfer(this.User.Id, dstId, amt, note, BankConstants.BankCharges.DifferentBankRTGSCharge, dstBankId);
                    break;
                case 4:
                    string dstBankId1 = this.Utility.GetStringInput("Enter destination account bank Id.");
                    isTransfered = this.AccountServices.OtherBankTransfer(this.User.Id, dstId, amt, note, BankConstants.BankCharges.DifferentBankIMPSCharge, dstBankId1);
                    break;
                default:
                    Console.WriteLine("Please select a valid option !!");
                    AccountHolderMenu();
                    break;
            }
            if (isTransfered) Console.WriteLine("Amount successfully transfered");
            else Console.WriteLine("Error occured ! Please try again");

            AccountHolderMenu();
        }

        public void RegisterMenu()
        {
            Console.Clear();
            int option = this.Utility.GetIntegerInput("Please select an account type :- \n" +
               " 1. Account holder account \n " +
               " 2. Employee account. \n " +
               " 3. Go Back");
            bool isRegistered = false;
            switch (option)
            {
                case 1:
                    this.GetUserDetails();
                    isRegistered = this.AdminServices.RegisterUser("AH", this.User);
                    break;
                case 2:
                    this.GetUserDetails();
                    isRegistered = this.AdminServices.RegisterUser("EMP", this.User);
                    break;
                default:
                    AdminMenu();
                    break;
            }
            if (isRegistered) Console.WriteLine("User successfully registered");
            else Console.WriteLine("Error occured ! Please try again.");

            AdminMenu();
        }

        public void AdminMenu()
        {
            Console.Clear();
            int option = this.Utility.GetIntegerInput("Hi! Welcome to the bank staff menu :- \n" +
                " 1. Create a new account. \n " +
                " 2. Update/Delete account. \n " +
                " 3. Add new currency. \n " +
                " 4. Add service charges for this bank. \n " +
                " 5. Add service charge for other bank. \n " +
                " 6. View transactions history. \n " +
                " 7. Revert a transaction.");
            switch (option)
            {
                case 1:
                    RegisterMenu();
                    break;
                case 2:
                    string accId = this.Utility.GetStringInput("Enter Transaction Id to be revereted.");
                    bool isDeletedSuccessfully = this.AdminServices.RevertTransaction(accId);

                    if (isDeletedSuccessfully) Console.WriteLine("Account successfully deleted.");
                    else Console.WriteLine("Error occurred!, Please try again");
                    break;
                case 3:
                    string symbol = this.Utility.GetStringInput("Enter currency symbol.");
                    string name = this.Utility.GetStringInput("Enter currency name.");
                    decimal exchangeRate = this.Utility.GetDecimalInput("Enter currency exchange rate.");
                    bool isAdded = this.AdminServices.AddNewCurrency(symbol, name, exchangeRate);

                    if (isAdded) Console.WriteLine("New Currency successfully added");
                    else Console.WriteLine("Error occurred!, Please try again");

                    AdminMenu();
                    break;
                case 4:
                    int rtgs = this.Utility.GetIntegerInput("Enter RTGS charges : ");
                    int imps = this.Utility.GetIntegerInput("Enter IMPS charges : ");
                    bool isUpdated = this.AdminServices.EditSameBankCharges(rtgs, imps);

                    if(isUpdated) Console.WriteLine("New Charges successfully added");
                    else Console.WriteLine("Error occurred!, Please try again");

                    AdminMenu();
                    break;
                case 5:
                    int rtgs1 = this.Utility.GetIntegerInput("Enter RTGS charges : ");
                    int imps1 = this.Utility.GetIntegerInput("Enter IMPS charges : ");
                    bool isUpdated1 = this.AdminServices.EditDiffBankCharges(rtgs1, imps1);

                    if (isUpdated1) Console.WriteLine("New Charges successfully added");
                    else Console.WriteLine("Error occurred!, Please try again");

                    AdminMenu();
                    break;
                case 6:
                    List<Transaction> transactions = this.AdminServices.DisplayTransactions();
                    bool isSuccessfull = this.DisplayTransactions(transactions);
                    if (isSuccessfull)
                        AdminMenu();
                    break;
                case 7:
                    string txnId = this.Utility.GetStringInput("Enter Transaction Id to be revereted.");
                    bool isRevertedSuccessfully = this.AdminServices.RevertTransaction(txnId);

                    if (isRevertedSuccessfully) Console.WriteLine("Transaction reverted successfully.");
                    else Console.WriteLine("Error occurred!, Please try again");

                    AdminMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please select a valid option !!");
                    AdminMenu();
                    break;
            }
        }
    }
}
