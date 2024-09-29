using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class Idea
    {
        public Idea()
        {
            Bills = new HashSet<Bill>();
            Comments = new HashSet<Comment>();
            Images = new HashSet<Image>();
            Likes = new HashSet<Like>();
            Votes = new HashSet<Vote>();
            Wishlists = new HashSet<Wishlist>();
        }

        public int IdeaId { get; set; }
        public DateTime IdeaDate { get; set; }
        public string Name { get; set; } = null!;
        public string Summary { get; set; } = null!;
        public string Content { get; set; } = null!;
        public int StatusId { get; set; }
        public int CateId { get; set; }
        public int UserId { get; set; }

        public virtual Category Cate { get; set; } = null!;
        public virtual Status Status { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Bill> Bills { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Image> Images { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
        public virtual ICollection<Wishlist> Wishlists { get; set; }
    }
}
