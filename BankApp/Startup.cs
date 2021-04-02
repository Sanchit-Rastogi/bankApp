using System;
using Microsoft.Extensions.DependencyInjection;
using BankApp.Services;
using BankApp.Interfaces;

namespace BankApp
{
    public class Startup
    {

        public static IServiceProvider ConfigurationService()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IBankService, BankService>()
                .AddSingleton<IAccountService, AccountService>()
                .AddSingleton<IAdminService, AdminService>()
                .AddSingleton<IUserService, UserService>()
                .BuildServiceProvider();

            return provider;
        }

    }
}
