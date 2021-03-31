using System;
using BankApp.Models;
using System.Collections.Generic;
using BankApp.Services.Data;
using System.Linq;

namespace BankApp.Services
{
    public class UserService
    {

        public string LoginUser(User inputUser)
        {
            try
            {
                using var db = new BankDBContext();
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
            catch (Exception)
            {
                return "Error Occurred";
            }
        }

    }
}
