using System;
using BankApp.Services;
using BankApp.Models;
using System.Collections.Generic;
using BankApp.Services.Utilities;
using BankApp.Services.Constants;

namespace BankApp
{
    public interface IBankApp
    {
        void Initialize();
        void DisplayMainMenu();
        User GetUserDetails();
        void AccountHolderMenu();
        bool DisplayTransactions(List<Transaction> transactions);
        void TransferFundsMenu();
        void RegisterMenu();
        void AdminMenu();
    }

    public class BankApplication : IBankApp
    {
        Utility Utility;
        User LoggedInUser;

        private readonly IBankService _bankService;
        private readonly IAdminService _adminService;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public BankApplication(IBankService bankService, IUserService userService, IAccountService accountService, IAdminService adminService)
        {
            _bankService = bankService;
            _accountService = accountService;
            _adminService = adminService;
            _userService = userService;
        }

        public BankApplication()
        {
            Initialize();
        }

        public void Initialize()
        {
            this.Utility = new Utility();
            this.LoggedInUser = new User();
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
                    Console.WriteLine("*** Registereing a new bank *** ");
                    string name = this.Utility.GetStringInput("Enter your bank name : ");
                    //bool isRegistered = this.BankServices.RegisterBank(name);
                    bool isRegistered = this._bankService.RegisterBank(name);
                    if (isRegistered) {
                        Console.WriteLine("New Bank Successfully Created.");
                        DisplayMainMenu();
                    }

                    break;
                case 2:
                    var user = this.GetUserDetails();
                    string loggedInStatus = this._userService.LoginUser(user);
                    if (loggedInStatus == "AccountHolder") {
                        this.LoggedInUser = user;
                        AccountHolderMenu();
                    }
                    else if (loggedInStatus == "Employee") {
                        this.LoggedInUser = user;

                    }
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
            return new User()
            {
                Name = this.Utility.GetStringInput("Enter your name"),
                Username = this.Utility.GetStringInput("Enter your username"),
                Password = this.Utility.GetStringInput("Enter your password"),
            };
        }

        public void AccountHolderMenu()
        {
            Console.Clear();
            int option = this.Utility.GetIntegerInput("Hi! Welcome to the Account Holder menu :- \n " +
                "1. Deposite Money. \n " +
                "2. Withdraw Money \n " +
                "3. Transfer Funds \n " +
                "4. View Transaction History \n " +
                "5. Logout");
            switch (option)
            {
                case 1:
                    decimal depositeAmount = this.Utility.GetDecimalInput("Enter amount to be deposited :- ");
                    bool isDeposited = this._accountService.Deposite(depositeAmount, this.LoggedInUser.Id);
                    if (isDeposited)
                        Console.WriteLine("Amount Successfully deposited");
                    else
                        Console.WriteLine("Unable to deposite amount, Try again later !");

                    this.AccountHolderMenu();

                    break;
                case 2:
                    decimal withdrawalAmount = this.Utility.GetDecimalInput("Enter amount to be withdrawn :- ");
                    string note = this.Utility.GetStringInput("Enter note for withdrawl :- ");
                    bool isWithdrawn = this._accountService.Withdrawal(withdrawalAmount, this.LoggedInUser.Id, note);
                    if (isWithdrawn)
                        Console.WriteLine("Amount Successfully withdrawn");
                    else
                        Console.WriteLine("Unable to withdraw amount, Try again later !");

                    this.AccountHolderMenu();

                    break;
                case 3:
                    this.TransferFundsMenu();

                    break;
                case 4:
                    List<Transaction> transactions = this._accountService.GetTransactions(this.LoggedInUser.Id);
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
            Console.WriteLine("TXN ID \t\t NOTE \t\t AMOUNT \t\t DATE \t\t REVERTED");
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"{transaction.TxnId} \t\t {transaction.Note} \t\t {transaction.Amount} \t\t {transaction.TxnDate} \t\t {transaction.IsRevereted}");
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
            int destinationAccountNumber = this.Utility.GetIntegerInput("Enter reciver account number.");
            decimal amount = this.Utility.GetDecimalInput("Enter amount to be tranfered.");
            string note = this.Utility.GetStringInput("Enter transfer note.");
            bool isTransfered = false;
            switch (option)
            {
                case 1:
                    isTransfered = this._accountService.Transfer(this.LoggedInUser.Id, destinationAccountNumber, amount, note, BankConstants.BankCharges.SameBankRTGS);

                    break;
                case 2:
                    isTransfered = this._accountService.Transfer(this.LoggedInUser.Id, destinationAccountNumber, amount, note, BankConstants.BankCharges.SameBankIMPS);

                    break;
                case 3:
                    isTransfered = this._accountService.Transfer(this.LoggedInUser.Id, destinationAccountNumber, amount, note, BankConstants.BankCharges.DifferentBankRTGS);

                    break;
                case 4:
                    isTransfered = this._accountService.Transfer(this.LoggedInUser.Id, destinationAccountNumber, amount, note, BankConstants.BankCharges.DifferentBankIMPS);

                    break;
                default:
                    Console.WriteLine("Please select a valid option !!");
                    AccountHolderMenu();

                    break;
            }
            if (isTransfered)
                Console.WriteLine("Amount successfully transfered");
            else
                Console.WriteLine("Error occured ! Please try again");

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
                    isRegistered = this._userService.RegisterUser("AH", this.LoggedInUser);

                    break;
                case 2:
                    this.GetUserDetails();
                    isRegistered = this._userService.RegisterUser("EMP", this.LoggedInUser);

                    break;
                default:
                    AdminMenu();

                    break;
            }
            if (isRegistered)
                Console.WriteLine("User successfully registered");
            else
                Console.WriteLine("Error occured ! Please try again.");

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
                    bool isDeletedSuccessfully = this._userService.DeleteAccount(accId);

                    if (isDeletedSuccessfully)
                        Console.WriteLine("Account successfully deleted.");
                    else
                        Console.WriteLine("Error occurred!, Please try again");

                    break;
                case 3:
                    string symbol = this.Utility.GetStringInput("Enter currency symbol.");
                    string name = this.Utility.GetStringInput("Enter currency name.");
                    decimal exchangeRate = this.Utility.GetDecimalInput("Enter currency exchange rate.");
                    bool isAdded = this._adminService.AddNewCurrency(symbol, name, exchangeRate);

                    if (isAdded)
                        Console.WriteLine("New Currency successfully added");
                    else
                        Console.WriteLine("Error occurred!, Please try again");

                    AdminMenu();

                    break;
                case 4:
                    int rtgs = this.Utility.GetIntegerInput("Enter RTGS charges : ");
                    int imps = this.Utility.GetIntegerInput("Enter IMPS charges : ");
                    bool isUpdated = this._adminService.SaveBankCharges(rtgs, imps, true);

                    if(isUpdated)
                        Console.WriteLine("New Charges successfully added");
                    else
                        Console.WriteLine("Error occurred!, Please try again");

                    AdminMenu();

                    break;
                case 5:
                    int rtgs1 = this.Utility.GetIntegerInput("Enter RTGS charges : ");
                    int imps1 = this.Utility.GetIntegerInput("Enter IMPS charges : ");
                    bool isUpdated1 = this._adminService.SaveBankCharges(rtgs1, imps1, false);

                    if (isUpdated1)
                        Console.WriteLine("New Charges successfully added");
                    else
                        Console.WriteLine("Error occurred!, Please try again");

                    AdminMenu();

                    break;
                case 6:
                    List<Transaction> transactions = this._adminService.GetAllTransactions();
                    bool isSuccessfull = this.DisplayTransactions(transactions);
                    if (isSuccessfull)
                        AdminMenu();

                    break;
                case 7:
                    string txnId = this.Utility.GetStringInput("Enter Transaction Id to be revereted.");
                    bool isRevertedSuccessfully = this._adminService.RevertTransaction(txnId);

                    if (isRevertedSuccessfully)
                        Console.WriteLine("Transaction reverted successfully.");
                    else
                        Console.WriteLine("Error occurred!, Please try again");

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
