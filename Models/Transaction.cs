using System;

namespace BankApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }

        public string TxnId { get; set; }

        public string Note { get; set; }

        public decimal Amount { get; set; }

        public DateTime TxnDate { get; set; }

        public int SourceId { get; set; }

        public int DestinationId { get; set; }
    }
}
