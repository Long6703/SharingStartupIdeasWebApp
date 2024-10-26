using System;
using System.Collections.Generic;

namespace SSI.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            IdeaInterests = new HashSet<IdeaInterest>();
            Ideas = new HashSet<Idea>();
            InvestmentRequests = new HashSet<InvestmentRequest>();
            Transactions = new HashSet<Transaction>();
        }

        public int UserId { get; set; }
        public string? Displayname { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<IdeaInterest> IdeaInterests { get; set; }
        public virtual ICollection<Idea> Ideas { get; set; }
        public virtual ICollection<InvestmentRequest> InvestmentRequests { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
