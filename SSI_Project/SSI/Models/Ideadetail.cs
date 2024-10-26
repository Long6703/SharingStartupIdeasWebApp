using System;
using System.Collections.Generic;

namespace SSI.Models
{
    public partial class Ideadetail
    {
        public Ideadetail()
        {
            Comments = new HashSet<Comment>();
            Images = new HashSet<Image>();
        }

        public int IdeaDetailId { get; set; }
        public int IdeaId { get; set; }
        public string Content { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }

        public virtual Idea Idea { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Image> Images { get; set; }
    }
}
