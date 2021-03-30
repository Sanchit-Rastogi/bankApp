using System;
using BankApp.Models;
using System.Collections.Generic;
using BankApp.Services.Data;
using System.Linq;

namespace BankApp.Services
{
    public class UserServices
    {

        public string LoginUser(User inputUser)
        {
            try
            {
                using(var db = new BankDBContext())
                {
                    var accHolder = from acc in db.AccountHolders
                                    where acc.User.Name == inputUser.Name && acc.User.Password == inputUser.Password
                                    select acc;
                    if (accHolder != null) return "Account Holder";
                    else
                    {
                        var employee = from emp in db.Employees
                                       where emp.User.Name == inputUser.Name && emp.User.Password == inputUser.Password
                                       select emp;
                        if (employee != null) return "Employee";
                        else return "User not found";
                    }
                }
            }
            catch (Exception)
            {
                return "Error Occurred";
            }
        } 

        //public bool RegisterUser(string role, User inputUser)
        //{
        //    if (role != "Emp")
        //    {
        //        Random r = new Random();
        //        AccountHolder accountHolder = new AccountHolder()
        //        {
        //            User.Name = inputUser.Name,
        //            Password = inputUser.Password,
        //            Username = inputUser.Username,
        //            AccId = "AH" + inputUser.Name[..3] + r.Next(10000).ToString(),
        //        };
        //        try
        //        {
        //            using (var db = new BankDBContext())
        //            {
        //                var accHolder = from acc in db.AccountHolders
        //                                where acc.Name == inputUser.Name && acc.Password == inputUser.Password
        //                                select acc;
        //                if (accHolder != null) return true;
        //                else
        //                {
        //                    db.AccountHolders.Add(accountHolder);
        //                    db.SaveChanges();
        //                    return true;
        //                }
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        Random r = new Random();
        //        Employee loginEmployee = new Employee()
        //        {
        //            Name = inputUser.Name,
        //            Password = inputUser.Password,
        //            EmployeeId = "EMP" + inputUser.Name[..3] + r.Next(10000).ToString(),
        //        };
        //        try
        //        {
        //            using (var db = new BankDBContext()) {
        //                var employee = from emp in db.Employees
        //                                where emp.Name == inputUser.Name && emp.Password == inputUser.Password
        //                                select emp;
        //                if (employee != null) return true;
        //                else
        //                {
        //                    db.Employees.Add(loginEmployee);
        //                    db.SaveChanges();
        //                    return true;
        //                }
        //            }
        //        }
        //        catch (Exception)
        //        {
        //            return false;
        //        }
        //    }
        //}
    }
}
