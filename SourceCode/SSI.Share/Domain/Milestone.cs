using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class Milestone
    {
        public int MilestoneId { get; set; }
        public int IdeaId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual Idea Idea { get; set; } = null!;
    }
}
