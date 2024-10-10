using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class ChatMessage
    {
        public int MessageId { get; set; }
        public int? SenderId { get; set; }
        public int? ReceiverId { get; set; }
        public string Message { get; set; } = null!;
        public DateTime? SentAt { get; set; }
        public string Status { get; set; } = null!;

        public virtual User? Receiver { get; set; }
        public virtual User? Sender { get; set; }
    }
}
