using System;
using BankApp.Models;
using BankApp.Services.Data;
using System.Linq;
using BankApp.Interfaces;
using BankApp.Services.Constants;

namespace BankApp.Services
{
    public class UserService : IUserService
    {

        public BankConstants.LoginStatus LoginUser(User inputUser)
        {
            try
            {
                var db = new BankDBContext();
                var accountHolder = from acc in db.AccountHolders
                                where acc.User.Name == inputUser.Name && acc.User.Password == inputUser.Password
                                select acc;
                if (accountHolder != null)
                    return BankConstants.LoginStatus.AccountHolder;
                else
                {
                    var employee = from emp in db.Employees
                                   where emp.User.Name == inputUser.Name && emp.User.Password == inputUser.Password
                                   select emp;
                    if (employee != null)
                        return BankConstants.LoginStatus.Employee;
                    else
                        return BankConstants.LoginStatus.UserNotFound;
                }
            }
            catch (Exception)
            {
                return BankConstants.LoginStatus.Error;
            }
        }

        public bool RegisterEmployee(User inputUser)
        {
            Employee loginEmployee = new Employee
            {
                User = inputUser,
                EmployeeId = "EMP" + inputUser.Name[..3] + DateTime.Now.ToLongDateString()
            };

            try
            {
                var db = new BankDBContext();
                var employee = from emp in db.Employees
                               where emp.User.Name == inputUser.Name && emp.User.Password == inputUser.Password
                               select emp;
                if (employee != null) return true;
                else
                {
                    db.Employees.Add(loginEmployee);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RegisterAccountHolder(User inputUser)
        {
            AccountHolder accountHolder = new AccountHolder
            {
                User = inputUser,
                AccId = "AH" + inputUser.Name[..3] + DateTime.Now.ToLongDateString()
            };
            try
            {
                var db = new BankDBContext();
                var accHolder = from acc in db.AccountHolders
                                where acc.User.Name == inputUser.Name && acc.User.Password == inputUser.Password
                                select acc;
                if (accHolder != null) return true;
                else
                {
                    db.AccountHolders.Add(accountHolder);
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteAccount(string accountId)
        {
            try
            {
                var db = new BankDBContext();
                db.AccountHolders.Remove(db.AccountHolders.SingleOrDefault(acc => acc.AccId == accountId));
                db.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

    }
}
