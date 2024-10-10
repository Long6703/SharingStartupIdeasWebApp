using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class ProjectUpdate
    {
        public int UpdateId { get; set; }
        public int IdeaId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Idea Idea { get; set; } = null!;
    }
}
