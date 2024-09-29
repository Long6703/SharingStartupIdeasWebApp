using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public string Image1 { get; set; } = null!;
        public int IdeaId { get; set; }

        public virtual Idea Idea { get; set; } = null!;
    }
}
