using System;
using System.Collections.Generic;

namespace SSI.Models
{
    public partial class IdeaInterest
    {
        public int InterestId { get; set; }
        public int UserId { get; set; }
        public int IdeaId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public virtual Idea Idea { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
