using System;
using System.Collections.Generic;

namespace SSI.Models
{
    public partial class InvestmentRequest
    {
        public InvestmentRequest()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int RequestId { get; set; }
        public int IdeaId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public decimal EquityPercentage { get; set; }
        public string InvestmentPeriod { get; set; } = null!;
        public string? Description { get; set; }

        public virtual Idea Idea { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
