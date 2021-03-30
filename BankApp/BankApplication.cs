using System;
using BankApp.Services;
using BankApp.Models;
using System.Collections.Generic;
using BankApp.Services.Utilities;

namespace BankApplication
{
    public class BankApplication
    {
        Utility Utility;
        BankServices BankServices;
        User User;
        UserServices UserServices;
        AccountServices AccountServices;
        BankCharges BankCharges;

        public BankApplication()
        {
            Initialize();
        }

        public void Initialize()
        {
            this.Utility = new Utility();
            this.BankServices = new BankServices();
            this.User = new User();
            this.UserServices = new UserServices();
            this.AccountServices = new AccountServices();
            this.BankCharges = new BankCharges();
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
                    else if (loggedInStatus == "Employee") BankStaffMenu();
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
                    Console.WriteLine("TXN ID \t\t NOTE \t\t AMOUNT \t\t DATE");
                    foreach (var transaction in transactions)
                    {
                        Console.WriteLine($"{transaction.TxnId} \t\t {transaction.Note} \t\t {transaction.Amount} \t\t {transaction.TxnDate}");
                    }
                    Console.WriteLine("\n Enter something to go back");
                    String result3 = Console.ReadLine();
                    switch (result3)
                    {
                        default:
                            Console.Clear();
                            AccountHolderMenu();
                            break;
                    }
                    break;
                case 5:
                    DisplayMainMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please select a valid option !!");
                    AccountHolderMenu();
                    break;
            }
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
                    isTransfered = this.AccountServices.Transfer(this.User.Id, dstId, amt, note, BankCharges.SameBankRTGSCharge);
                    break;
                case 2:
                    isTransfered = this.AccountServices.Transfer(this.User.Id, dstId, amt, note, BankCharges.SameBankIMPSCharge);
                    break;
                case 3:
                    string dstBankId = this.Utility.GetStringInput("Enter destination account bank Id.");
                    isTransfered = this.AccountServices.OtherBankTransfer(this.User.Id, dstId, amt, note, BankCharges.DifferentBankRTGSCharge, dstBankId);
                    break;
                case 4:
                    string dstBankId1 = this.Utility.GetStringInput("Enter destination account bank Id.");
                    isTransfered = this.AccountServices.OtherBankTransfer(this.User.Id, dstId, amt, note, BankCharges.DifferentBankIMPSCharge, dstBankId1);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please select a valid option !!");
                    AccountHolderMenu();
                    break;
            }
            if (isTransfered) Console.WriteLine("Amount successfully transfered");
            else Console.WriteLine("Error occured ! Please try again");

            AccountHolderMenu();
        }

        public void BankStaffMenu()
        {
            Console.Clear();
            Console.WriteLine("Hi! Welcome to the bank staff menu :- \n");
            Console.WriteLine("1. Create a new account.");
            Console.WriteLine("2. Update/Delete account.");
            Console.WriteLine("3. Add new currency.");
            Console.WriteLine("4. Add service charges for this bank.");
            Console.WriteLine("5. Add service charge for other bank.");
            Console.WriteLine("6. View transactions history.");
            Console.WriteLine("7. Revert a transaction.");
            int option = Convert.ToInt32(Console.ReadLine());
            switch (option)
            {
                case 1:
                    //DisplayLoginMenu();
                    break;
                case 2:
                    Console.WriteLine("Add service charges for this bank.");
                    break;
                case 3:
                    Console.WriteLine("Add service charges for this bank.");
                    break;
                case 4:
                    Console.WriteLine("Add service charges for this bank.");
                    break;
                case 5:
                    Console.WriteLine("Add service charge for other bank.");
                    break;
                case 6:
                    Console.WriteLine("View transactions history.");
                    break;
                case 7:
                    Console.WriteLine("Revert a transaction.");
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please select a valid option !!");
                    BankStaffMenu();
                    break;
            }
        }
    }
}
