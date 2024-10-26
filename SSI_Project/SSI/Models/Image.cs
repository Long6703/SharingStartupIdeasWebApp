using System;
using System.Collections.Generic;

namespace SSI.Models
{
    public partial class Image
    {
        public int ImageId { get; set; }
        public int IdeaDetailId { get; set; }
        public string Url { get; set; } = null!;

        public virtual Ideadetail IdeaDetail { get; set; } = null!;
    }
}
