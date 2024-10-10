using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class MediaDescription
    {
        public int MediaId { get; set; }
        public int? IdeaId { get; set; }
        public string MediaType { get; set; } = null!;
        public string MediaUrl { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual Idea? Idea { get; set; }
    }
}
