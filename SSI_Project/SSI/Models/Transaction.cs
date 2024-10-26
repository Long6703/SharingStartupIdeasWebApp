using System;
using System.Collections.Generic;

namespace SSI.Models
{
    public partial class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int IdeaId { get; set; }
        public decimal Amount { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? TransactionCode { get; set; }

        public virtual Idea Idea { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
