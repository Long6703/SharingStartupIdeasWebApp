using System;
using System.Collections.Generic;

namespace SSI.Models
{
    public partial class Idea
    {
        public Idea()
        {
            IdeaInterests = new HashSet<IdeaInterest>();
            Ideadetails = new HashSet<Ideadetail>();
            InvestmentRequests = new HashSet<InvestmentRequest>();
            Transactions = new HashSet<Transaction>();
        }

        public int IdeaId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Status { get; set; }
        public bool? IsSeekingInvestment { get; set; }
        public bool? IsImplement { get; set; }
        public string? PosterImg { get; set; }

        public virtual Category? Category { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<IdeaInterest> IdeaInterests { get; set; }
        public virtual ICollection<Ideadetail> Ideadetails { get; set; }
        public virtual ICollection<InvestmentRequest> InvestmentRequests { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
