using System;
using BankApp.Models;
using BankApp.Services.Constants;

namespace BankApp.Interfaces
{
    public interface IUserService
    {
        BankConstants.LoginStatus LoginUser(User inputUser);

        bool RegisterAccountHolder(User inputUser);

        bool RegisterEmployee(User inputUser);

        bool DeleteAccount(string accountId);
    }
}
