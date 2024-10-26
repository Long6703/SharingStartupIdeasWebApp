using System;
using System.Collections.Generic;

namespace SSI.Models
{
    public partial class Category
    {
        public Category()
        {
            Ideas = new HashSet<Idea>();
        }

        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Idea> Ideas { get; set; }
    }
}
