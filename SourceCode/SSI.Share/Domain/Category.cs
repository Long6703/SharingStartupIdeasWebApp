using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class Category
    {
        public Category()
        {
            Ideas = new HashSet<Idea>();
            Subcategories = new HashSet<Subcategory>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual ICollection<Idea> Ideas { get; set; }
        public virtual ICollection<Subcategory> Subcategories { get; set; }
    }
}
