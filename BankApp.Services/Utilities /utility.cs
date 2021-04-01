using System;
using System.Text.RegularExpressions;

namespace BankApp.Services.Utilities
{
    public class Utility
    {
        public string GetStringInput(string displayText)
        {
            Console.WriteLine(displayText);
            string userInput = Console.ReadLine();

            if (Regex.IsMatch(userInput, "[a-z]|[A-Z]")) return userInput;
            else
            {
                Console.WriteLine("Please enter a valid input !");
                return this.GetStringInput(displayText);
            }
        }

        public int GetIntegerInput(string displayText)
        {
            Console.WriteLine(displayText);
            int userInput = Convert.ToInt32(Console.ReadLine());

            return userInput;
            //Regex regex = new Regex(@"^\d$");
            //if (regex.Match()) return userInput;
            //else
            //{
            //    Console.WriteLine("Please enter a valid input !");
            //    return this.GetIntegerInput(displayText);
            //}
        }

        public decimal GetDecimalInput(string displayText)
        {
            Console.WriteLine(displayText);

            return Convert.ToInt32(Console.ReadLine());
        }
    }
}
