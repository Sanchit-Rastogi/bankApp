using System;
namespace BankApp.Models
{
    public class Currency
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public decimal ExchangeRate { get; set; }

        public bool IsDefault { get; set; }
    }
}
