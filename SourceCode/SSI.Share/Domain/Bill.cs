using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class Bill
    {
        public int BillId { get; set; }
        public DateTime BillDate { get; set; }
        public double BillAmount { get; set; }
        public int UserId { get; set; }
        public int IdeaId { get; set; }

        public virtual Idea Idea { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
