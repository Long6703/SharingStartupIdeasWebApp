using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int? UserId { get; set; }
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? TransactionDate { get; set; }

        public virtual User? User { get; set; }
    }
}
