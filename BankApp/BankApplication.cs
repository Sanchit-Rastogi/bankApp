using System;
using BankApp.Services;
using BankApp.Models;
using System.Collections.Generic;

namespace BankApplication
{
    public class BankApplication
    {

        public BankApplication()
        {
            Initialize();
        }

        public void Initialize()
        {
            DisplayMainMenu();
        }

        public void DisplayMainMenu()
        {
            //Console.Clear();
            Console.WriteLine("Welcome to The Bank Application Project \n \n");
            Console.WriteLine("Please enter option from the list :- \n");
            Console.WriteLine("1. Create a Bank");
            Console.WriteLine("2. Login for Staff / Account holder");
            Console.WriteLine("3. Exit");
            int option = Convert.ToInt32(Console.ReadLine());
            BankServices bankServices = new BankServices();
            switch (option)
            {
                case 1:
                    Console.WriteLine("Registereing a new bank");
                    Console.WriteLine("Enter your bank name");
                    string name = Console.ReadLine();
                    bool result = bankServices.RegisterBank(name);
                    if (result) {
                        Console.WriteLine("New Bank Successfully Created.");
                        DisplayMainMenu();
                    }
                    break;
                case 2:
                    DisplayLoginMenu();
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
            User user = new User();
            Console.WriteLine("Enter your name");
            user.Name = Console.ReadLine();
            Console.WriteLine("Enter your username");
            user.Username = Console.ReadLine();
            Console.WriteLine("Enter your password");
            user.Password = Console.ReadLine();
            return user;
        }

        public void DisplayLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("Please enter option from the list :- \n");
            Console.WriteLine("1. Login as a Account Holder");
            Console.WriteLine("2. Login as a Employee");
            Console.WriteLine("3. Go Back to main menu");
            int option = Convert.ToInt32(Console.ReadLine());
            UserLoginServices userLogin = new UserLoginServices();
            switch (option)
            {
                case 1:
                    User loginUser = GetUserDetails();
                    bool result = userLogin.LoginUser("AH", loginUser);
                    if (result) AccountHolderMenu();
                    break;
                case 2:
                    User loginUser1 = GetUserDetails();
                    bool result1 = userLogin.LoginUser("Emp", loginUser1);
                    if (result1) BankStaffMenu();
                    break;
                case 3:
                    Console.Clear();
                    DisplayMainMenu();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Please select a valid option !!");
                    DisplayLoginMenu();
                    break;
            }
        } 

        public void AccountHolderMenu()
        {
            AccountServices accountServices = new AccountServices();
            Console.WriteLine("Hi! Welcome to the Account Holder menu :- \n");
            Console.WriteLine("1. Deposite Money.");
            Console.WriteLine("2. Withdraw Money");
            Console.WriteLine("3. Transfer Funds");
            Console.WriteLine("4. View Transaction History");
            Console.WriteLine("5. Go back to main menu");
            int option = Convert.ToInt32(Console.ReadLine());
            Transaction txn = new Transaction();
            switch (option)
            {
                case 1:
                    Console.WriteLine("Enter account Id :- ");
                    txn.SourceId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter amount to be deposited :- ");
                    txn.Amount = Convert.ToInt32(Console.ReadLine());
                    txn.Note = "Made a deposte";
                    txn.TxnDate = DateTime.Now;
                    txn.DestinationId = txn.SourceId;
                    bool result = accountServices.Deposite(txn);
                    if (result)
                    {
                        Console.Clear();
                        Console.WriteLine("Amount Successfully deposited");
                        AccountHolderMenu();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Unable to deposite amount, Try again later !");
                        AccountHolderMenu();
                    }
                    break;
                case 2:
                    Console.WriteLine("Enter account Id :- ");
                    txn.SourceId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter amount to be withdrawn :- ");
                    txn.Amount = - Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter note for withdrawl :- ");
                    txn.Note = Console.ReadLine();
                    txn.TxnDate = DateTime.Now;
                    txn.DestinationId = txn.SourceId;
                    bool result1 = accountServices.Withdrawal(txn);
                    if (result1)
                    {
                        Console.Clear();
                        Console.WriteLine("Amount Successfully withdrawn");
                        AccountHolderMenu();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Unable to withdraw amount, Try again later !");
                        AccountHolderMenu();
                    }
                    break;
                case 3:
                    Console.WriteLine("Enter your account Id :- ");
                    txn.SourceId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter reciver account Id :- ");
                    txn.DestinationId = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter amount to be transfered :- ");
                    txn.Amount = -Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Enter note for transfer :- ");
                    txn.Note = Console.ReadLine();
                    txn.TxnDate = DateTime.Now;
                    bool result2 = accountServices.Transfer(txn);
                    if (result2)
                    {
                        Console.Clear();
                        Console.WriteLine("Amount Successfully transfered");
                        AccountHolderMenu();
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Unable to transfer amount, Try again later !");
                        AccountHolderMenu();
                    }
                    break;
                case 4:
                    List<Transaction> transactions = accountServices.DisplayTransactions();
                    Console.WriteLine("TXN ID \t\t NOTE \t\t AMOUNT \t\t DATE");
                    foreach (var transaction in transactions)
                    {
                        Console.WriteLine($"{transaction.TxnId} \t\t {transaction.Note} \t\t {transaction.Amount} \t\t {transaction.TxnDate}");
                    }
                    Console.WriteLine("");
                    Console.WriteLine("Enter something to go back");
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
            //BankStaffServices bankStaffServices = new BankStaffServices();
            switch (option)
            {
                case 1:
                    DisplayLoginMenu();
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
