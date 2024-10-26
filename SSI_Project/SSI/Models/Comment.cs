using System;
using System.Collections.Generic;

namespace SSI.Models
{
    public partial class Comment
    {
        public Comment()
        {
            InverseParent = new HashSet<Comment>();
        }

        public int CommentId { get; set; }
        public int IdeaDetailId { get; set; }
        public int UserId { get; set; }
        public int? ParentId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual Ideadetail IdeaDetail { get; set; } = null!;
        public virtual Comment? Parent { get; set; }
        public virtual User User { get; set; } = null!;
        public virtual ICollection<Comment> InverseParent { get; set; }
    }
}
