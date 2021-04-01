using System;
using Microsoft.Extensions.DependencyInjection;
using BankApp.Services;

namespace BankApp
{
    public class Startup
    {

        public static IServiceProvider ConfigurationService()
        {
            var provider = new ServiceCollection()
                .AddSingleton<IBankService, BankService>()
                .AddSingleton<IBankApp, BankApplication>()
                .AddSingleton<IAccountService, AccountService>()
                .AddSingleton<IAdminService, AdminService>()
                .AddSingleton<IUserService, UserService>()
                .BuildServiceProvider();

            return provider;
        }

    }
}
