using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class User
    {
        public User()
        {
            ChatMessageReceivers = new HashSet<ChatMessage>();
            ChatMessageSenders = new HashSet<ChatMessage>();
            Comments = new HashSet<Comment>();
            IdeaInterests = new HashSet<IdeaInterest>();
            Ideas = new HashSet<Idea>();
            Notifications = new HashSet<Notification>();
            Transactions = new HashSet<Transaction>();
        }

        public int UserId { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public string? ProfileImageUrl { get; set; }
        public string? Bio { get; set; }
        public string Status { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public int? RoleId { get; set; }

        public virtual Role? Role { get; set; }
        public virtual ICollection<ChatMessage> ChatMessageReceivers { get; set; }
        public virtual ICollection<ChatMessage> ChatMessageSenders { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<IdeaInterest> IdeaInterests { get; set; }
        public virtual ICollection<Idea> Ideas { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
