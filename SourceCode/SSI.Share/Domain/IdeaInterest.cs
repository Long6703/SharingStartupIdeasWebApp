using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class IdeaInterest
    {
        public int InterestId { get; set; }
        public int? UserId { get; set; }
        public int? IdeaId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual Idea? Idea { get; set; }
        public virtual User? User { get; set; }
    }
}
