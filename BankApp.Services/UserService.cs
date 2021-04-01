using System;
using BankApp.Models;
using BankApp.Services.Data;
using System.Linq;

namespace BankApp.Services
{
    public interface IUserService
    {
        string LoginUser(User inputUser);

        bool RegisterUser(string role, User inputUser);

        bool DeleteAccount(string accountId);
    }

    public class UserService : IUserService
    {

        public string LoginUser(User inputUser)
        {
            try
            {
                var db = new BankDBContext();
                var accountHolder = from acc in db.AccountHolders
                                where acc.User.Name == inputUser.Name && acc.User.Password == inputUser.Password
                                select acc;
                if (accountHolder != null) return "Account_Holder";
                else
                {
                    var employee = from emp in db.Employees
                                   where emp.User.Name == inputUser.Name && emp.User.Password == inputUser.Password
                                   select emp;
                    if (employee != null) return "Employee";
                    else return "User not found";
                }
            }
            catch (Exception)
            {
                return "Error Occurred";
            }
        }

        public bool RegisterUser(string role, User inputUser)
        {
            if (role == "AH")
            {
                AccountHolder accountHolder = new AccountHolder();
                accountHolder.User.Name = inputUser.Name;
                accountHolder.User.Password = inputUser.Password;
                accountHolder.User.Username = inputUser.Username;
                accountHolder.AccId = "AH" + inputUser.Name[..3] + DateTime.Now.ToLongDateString();
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
            else
            {
                Employee loginEmployee = new Employee();
                loginEmployee.User.Name = inputUser.Name;
                loginEmployee.User.Username = inputUser.Username;
                loginEmployee.User.Password = inputUser.Password;
                loginEmployee.EmployeeId = "EMP" + inputUser.Name[..3] + DateTime.Now.ToLongDateString();

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
