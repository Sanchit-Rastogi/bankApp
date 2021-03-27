using System;
using BankApp.Models;
using System.Collections.Generic;
using BankApp.Services.Data;

namespace BankApp.Services
{
    public class UserLoginServices
    {
        //Fetch these data from the db
        List<AccountHolder> AccountHolders = new List<AccountHolder>();
        List<Employee> Employees = new List<Employee>();

        public bool LoginUser(string role, User inputUser)
        {
            if (role != "Emp")
            {
                Random r = new Random();
                AccountHolder accountHolder = new AccountHolder()
                {
                    Name = inputUser.Name,
                    Password = inputUser.Password,
                    Username = inputUser.Username,
                    AccId = "AH" + inputUser.Name[..3] + r.Next(10000).ToString(),
                };
                try
                {
                    var accHolder = AccountHolders.Find(e => e.Name == inputUser.Name && e.Password == inputUser.Password);
                    if (accHolder != null) return true;
                    else
                    {
                        using (var db = new BankDBContext())
                        {
                            db.AccountHolders.Add(accountHolder);
                            db.SaveChanges();
                        }
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                Random r = new Random();
                Employee loginEmployee = new Employee()
                {
                    Name = inputUser.Name,
                    Password = inputUser.Password,
                    EmployeeId = "EMP" + inputUser.Name[..3] + r.Next(10000).ToString(),
                };
                try
                {
                    var employee = Employees.Find(e => e.Name == inputUser.Name && e.Password == inputUser.Password);
                    if (employee != null) return true;
                    else
                    {
                        using (var db = new BankDBContext())
                        {
                            db.Employees.Add(loginEmployee);
                            db.SaveChanges();
                        }
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
    }
}
