using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class Category
    {
        public Category()
        {
            Ideas = new HashSet<Idea>();
        }

        public int CateId { get; set; }
        public string Category1 { get; set; } = null!;

        public virtual ICollection<Idea> Ideas { get; set; }
    }
}
