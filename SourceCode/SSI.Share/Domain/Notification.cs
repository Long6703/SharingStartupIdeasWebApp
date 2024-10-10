using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int? UserId { get; set; }
        public string Message { get; set; } = null!;
        public bool? IsRead { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual User? User { get; set; }
    }
}
