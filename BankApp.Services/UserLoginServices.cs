using System;
using BankApp.Models;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using BankApp.Services.Data;

namespace BankApp.Services
{
    public class UserLoginServices
    {
        BankModel BankData = new BankModel();

        public bool LoginUser(String role)
        {
            // Getting data from XML
            XmlSerializer serializer = new XmlSerializer(typeof(BankModel));
            FileStream fs = new FileStream("bankData.xml", FileMode.Open);
            BankData = (BankModel)serializer.Deserialize(fs);

            // Logging In User
            Console.WriteLine("Enter your user name");
            string name = Console.ReadLine();
            Console.WriteLine("Enter your password");
            string password = Console.ReadLine();
            if (role != "Emp")
            {
                AccountHolder accountHolder = new AccountHolder()
                {
                    Name = name,
                    Password = password,
                };
                AccountServices accountServices = new AccountServices();
                bool flag = false;
                foreach (var user in BankData.AccountHolders)
                {
                    flag = true;
                    if (user.Name == name && user.Password == password)
                    {
                        accountServices.AccId = user.Id;
                        Console.Clear();
                        Console.WriteLine("User successfully logged In");
                        return true;
                    }
                    else
                    {
                        accountHolder.Id = name[..3] + DateTime.Now.ToShortTimeString();
                        accountHolder.AccId = BankData.Name[..3] + name[..3] + DateTime.Now.ToShortDateString();
                        accountHolder.Balance = 0;
                        BankData.AccountHolders.Add(accountHolder);
                        SaveToXML();
                        AddAccountHolderToDb(accountHolder);
                        accountServices.AccId = accountHolder.AccId;
                        Console.Clear();
                        Console.WriteLine("New User successfully logged In");
                        return true;
                    }
                }
                if (!flag)
                {
                    accountHolder.Id = name[..3] + DateTime.Now.ToShortTimeString();
                    accountHolder.AccId = BankData.Name[..3] + accountHolder.Name[..3] + DateTime.Now.ToShortDateString();
                    accountHolder.Balance = 0;
                    BankData.AccountHolders.Add(accountHolder);
                    SaveToXML();
                    AddAccountHolderToDb(accountHolder);
                    accountServices.AccId = accountHolder.AccId;
                    Console.Clear();
                    Console.WriteLine("New User successfully logged In");
                    return true;
                }
            }
            else
            {
                Employee loginEmployee = new Employee()
                {
                    Name = name,
                    Password = password,
                    EmployeeId = "EMP" + name[..3] + DateTime.Now.ToShortTimeString(),
                };
                foreach (var employee in BankData.Employees)
                {
                    if (employee.Name == name && employee.Password == password)
                    {
                        Console.Clear();
                        Console.WriteLine("Employee successfully logged In");
                        return true;
                    }
                    else
                    {
                        BankData.Employees.Add(loginEmployee);
                        SaveToXML();
                        AddEmployeeToDb(loginEmployee);
                        Console.Clear();
                        Console.WriteLine("Employee successfully logged In");
                        return true;
                    }
                }
            }
            return false;
        }

        public void AddAccountHolderToDb(AccountHolder accountHolder)
        {
            using (var db = new BankDBContext())
            {
                db.AccountHolders.Add(accountHolder);
                db.SaveChanges();
            }
        }

        public void AddEmployeeToDb(Employee employee)
        {
            using (var db = new BankDBContext())
            {
                db.Employees.Add(employee);
                db.SaveChanges();
            }
        }

        public void SaveToXML()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(BankModel));
            TextWriter writer = new StreamWriter("bankData.xml");
            serializer.Serialize(writer, BankData);
            writer.Close();
        }
    }
}
