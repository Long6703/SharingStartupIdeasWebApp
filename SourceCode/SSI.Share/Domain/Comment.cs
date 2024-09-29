using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string Content { get; set; } = null!;
        public int UserId { get; set; }
        public int IdeaId { get; set; }

        public virtual Idea Idea { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
