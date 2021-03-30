using System;

namespace BankApp.Services.Utilities
{
    public class Utility
    {
        public string GetStringInput(string displayText)
        {
            Console.WriteLine(displayText);

            return Console.ReadLine();
        }

        public int GetIntegerInput(string displayText)
        {
            Console.WriteLine(displayText);

            return Convert.ToInt32(Console.ReadLine());
        }

        public decimal GetDecimalInput(string displayText)
        {
            Console.WriteLine(displayText);

            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
