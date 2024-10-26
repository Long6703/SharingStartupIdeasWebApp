using System;
using System.Collections.Generic;

namespace SSI.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int InvestmentRequestId { get; set; }
        public decimal Amount { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? TransactionCode { get; set; }

        public virtual InvestmentRequest InvestmentRequest { get; set; } = null!;
    }
}
