using BankApp;
using Microsoft.Extensions.DependencyInjection;

namespace BankApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = Startup.ConfigurationService();
            var BankApp = container.GetRequiredService<IBankApp>();

            BankApp.Initialize();
        }
    }
}