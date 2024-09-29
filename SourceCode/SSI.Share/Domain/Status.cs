using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class Status
    {
        public Status()
        {
            Ideas = new HashSet<Idea>();
        }

        public int StatusId { get; set; }
        public string Status1 { get; set; } = null!;

        public virtual ICollection<Idea> Ideas { get; set; }
    }
}
