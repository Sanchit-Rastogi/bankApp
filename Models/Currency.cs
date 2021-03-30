using System;
namespace BankApp.Models
{
    public class Currency
    {
        public int Id { get; set; }

        public string Symbol { get; set; }

        public string Name { get; set; }

        public decimal ConversionRate { get; set; }

        public decimal IsDefault { get; set; }
    }
}
