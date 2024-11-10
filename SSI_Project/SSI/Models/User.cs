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
        }

        public int UserId { get; set; }
        public string? Displayname { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? Role { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Bio { get; set; }
        public string? Location { get; set; }
        public string? Profession { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? LinkedinUrl { get; set; }
        public string? TwitterUrl { get; set; }
        public string? FacebookUrl { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? BankName { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<IdeaInterest> IdeaInterests { get; set; }
        public virtual ICollection<Idea> Ideas { get; set; }
        public virtual ICollection<InvestmentRequest> InvestmentRequests { get; set; }
    }
}
