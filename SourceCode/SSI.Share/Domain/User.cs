using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class User
    {
        public User()
        {
            Bills = new HashSet<Bill>();
            Comments = new HashSet<Comment>();
            Ideas = new HashSet<Idea>();
            Likes = new HashSet<Like>();
            Votes = new HashSet<Vote>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phonenumber { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool Status { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Idea> Ideas { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
