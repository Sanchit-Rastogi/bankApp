using System;
using BankApp.Services;
using BankApp.Models;
using System.Collections.Generic;
using BankApp.Services.Utilities;
using BankApp.Services.Constants;
using BankApp.Interfaces;

namespace BankApp
{
    public class BankApplication
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
            Startup.ConfigurationService();
            this.Utility = new Utility();
            this.LoggedInUser = new User();
            MainMenu();
        }

        public void MainMenu()
        {
            Console.WriteLine(BankConstants.MainMenuOptions);

            BankConstants.MenuOptions option = (BankConstants.MenuOptions) this.Utility.GetIntegerInput(" Please enter option from the list :- \n ");
            switch (option)
            {
                case BankConstants.MenuOptions.RegisterBank:
                    Console.WriteLine("*** Registereing a new bank *** ");
                    string name = this.Utility.GetStringInput("Enter your bank name : ");
                    bool isRegistered = this._bankService.RegisterBank(name);
                    if (isRegistered) {
                        Console.WriteLine("New Bank Successfully Created.");
                        MainMenu();
                    }

                    break;
                case BankConstants.MenuOptions.Login:
                    var user = this.GetUserDetails();
                    BankConstants.LoginStatus loggedInStatus = this._userService.LoginUser(user);
                    if (loggedInStatus == BankConstants.LoginStatus.AccountHolder) {
                        this.LoggedInUser = user;
                        this.AccountHolderMenu();
                    }
                    else if (loggedInStatus == BankConstants.LoginStatus.Employee) {
                        this.LoggedInUser = user;
                        this.AdminMenu();
                    }
                    else if (loggedInStatus == BankConstants.LoginStatus.UserNotFound) {
                        Console.WriteLine("Login error ! User not found.");
                        MainMenu();
                    }
                    else
                    {
                        Console.WriteLine("Login error ! Please try again.");
                        MainMenu();
                    }

                    break;
                case BankConstants.MenuOptions.Exit:
                    Environment.Exit(0);

                    break;
                default:
                    Console.WriteLine("Please select a valid option !!");
                    MainMenu();

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
            BankConstants.AccountHolderOptions option = (BankConstants.AccountHolderOptions)this.Utility.GetIntegerInput(BankConstants.AccountHolderMenuOptions);
            switch (option)
            {
                case BankConstants.AccountHolderOptions.Deposite:
                    decimal depositeAmount = this.Utility.GetDecimalInput("Enter amount to be deposited :- ");
                    bool isDeposited = this._accountService.Deposite(depositeAmount, this.LoggedInUser.Id);
                    if (isDeposited)
                        Console.WriteLine("Amount Successfully deposited");
                    else
                        Console.WriteLine("Unable to deposite amount, Try again later !");

                    this.AccountHolderMenu();

                    break;
                case BankConstants.AccountHolderOptions.Withdraw:
                    decimal withdrawalAmount = this.Utility.GetDecimalInput("Enter amount to be withdrawn :- ");
                    string note = this.Utility.GetStringInput("Enter note for withdrawl :- ");
                    bool isWithdrawn = this._accountService.Withdrawal(withdrawalAmount, this.LoggedInUser.Id, note);
                    if (isWithdrawn)
                        Console.WriteLine("Amount Successfully withdrawn");
                    else
                        Console.WriteLine("Unable to withdraw amount, Try again later !");

                    this.AccountHolderMenu();

                    break;
                case BankConstants.AccountHolderOptions.Transfer:
                    this.TransferFundsMenu();

                    break;
                case BankConstants.AccountHolderOptions.Transactions:
                    List<Transaction> transactions = this._accountService.GetTransactions(this.LoggedInUser.Id);
                    bool isSuccessfull = DisplayTransactions(transactions);
                    if (isSuccessfull)
                        AccountHolderMenu();

                    break;
                case BankConstants.AccountHolderOptions.Logout:
                    MainMenu();

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
            BankConstants.TransferMenuOptions option = (BankConstants.TransferMenuOptions)this.Utility.GetIntegerInput(BankConstants.TransferFundsMenuOptions);
            int destinationAccountNumber = this.Utility.GetIntegerInput("Enter reciver account number.");
            decimal amount = this.Utility.GetDecimalInput("Enter amount to be tranfered.");
            string note = this.Utility.GetStringInput("Enter transfer note.");
            bool isTransfered = false;
            switch (option)
            {
                case BankConstants.TransferMenuOptions.SameRTGS:
                    isTransfered = this._accountService.Transfer(this.LoggedInUser.Id, destinationAccountNumber, amount, note, BankConstants.BankCharges.SameBankRTGS);

                    break;
                case BankConstants.TransferMenuOptions.SameIMPS:
                    isTransfered = this._accountService.Transfer(this.LoggedInUser.Id, destinationAccountNumber, amount, note, BankConstants.BankCharges.SameBankIMPS);

                    break;
                case BankConstants.TransferMenuOptions.DIffRTGS:
                    isTransfered = this._accountService.Transfer(this.LoggedInUser.Id, destinationAccountNumber, amount, note, BankConstants.BankCharges.DifferentBankRTGS);

                    break;
                case BankConstants.TransferMenuOptions.DiffTMPS:
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
            BankConstants.RegisterOptionsMenu option = (BankConstants.RegisterOptionsMenu)this.Utility.GetIntegerInput(BankConstants.RegisterMenuOptions);
            bool isRegistered = false;
            User registerUser = new User(); 
            switch (option)
            {
                case BankConstants.RegisterOptionsMenu.AccountHolder:
                    registerUser = this.GetUserDetails();
                    isRegistered = this._userService.RegisterAccountHolder(registerUser);

                    break;
                case BankConstants.RegisterOptionsMenu.Employee:
                    registerUser = this.GetUserDetails();
                    isRegistered = this._userService.RegisterEmployee(registerUser);

                    break;
                default:
                    AdminMenu();

                    break;
            }
            if (isRegistered)
            {
                Console.WriteLine("User successfully registered");
                this.LoggedInUser = registerUser;
            }
            else
                Console.WriteLine("Error occured ! Please try again.");

            AdminMenu();
        }

        public void AdminMenu()
        {
            Console.Clear();
            BankConstants.AdminOptionsMenu option = (BankConstants.AdminOptionsMenu)this.Utility.GetIntegerInput(BankConstants.AdminMenuOptions);
            switch (option)
            {
                case BankConstants.AdminOptionsMenu.Register:
                    RegisterMenu();

                    break;
                case BankConstants.AdminOptionsMenu.DeleteAccount:
                    string accId = this.Utility.GetStringInput("Enter Transaction Id to be revereted.");
                    bool isDeletedSuccessfully = this._userService.DeleteAccount(accId);

                    if (isDeletedSuccessfully)
                        Console.WriteLine("Account successfully deleted.");
                    else
                        Console.WriteLine("Error occurred!, Please try again");

                    break;
                case BankConstants.AdminOptionsMenu.NewCurrency:
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
                case BankConstants.AdminOptionsMenu.SameBankService:
                    int rtgs = this.Utility.GetIntegerInput("Enter RTGS charges : ");
                    int imps = this.Utility.GetIntegerInput("Enter IMPS charges : ");
                    bool isUpdated = this._adminService.SaveBankCharges(rtgs, imps, true);

                    if(isUpdated)
                        Console.WriteLine("New Charges successfully added");
                    else
                        Console.WriteLine("Error occurred!, Please try again");

                    AdminMenu();

                    break;
                case BankConstants.AdminOptionsMenu.DiffBankService:
                    int rtgs1 = this.Utility.GetIntegerInput("Enter RTGS charges : ");
                    int imps1 = this.Utility.GetIntegerInput("Enter IMPS charges : ");
                    bool isUpdated1 = this._adminService.SaveBankCharges(rtgs1, imps1, false);

                    if (isUpdated1)
                        Console.WriteLine("New Charges successfully added");
                    else
                        Console.WriteLine("Error occurred!, Please try again");

                    AdminMenu();

                    break;
                case BankConstants.AdminOptionsMenu.Transactions:
                    List<Transaction> transactions = this._adminService.GetAllTransactions();
                    bool isSuccessfull = this.DisplayTransactions(transactions);
                    if (isSuccessfull)
                        AdminMenu();

                    break;
                case BankConstants.AdminOptionsMenu.RevertTransactions:
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
