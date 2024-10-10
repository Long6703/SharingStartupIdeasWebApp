using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class Subcategory
    {
        public Subcategory()
        {
            Ideas = new HashSet<Idea>();
        }

        public int SubcategoryId { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual Category? Category { get; set; }
        public virtual ICollection<Idea> Ideas { get; set; }
    }
}
