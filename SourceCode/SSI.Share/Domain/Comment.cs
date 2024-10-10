using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public int? UserId { get; set; }
        public int? IdeaId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual Idea? Idea { get; set; }
        public virtual User? User { get; set; }
    }
}
