using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class Report
    {
        public int ReportId { get; set; }
        public string ReportType { get; set; } = null!;
        public string Details { get; set; } = null!;
        public DateTime? CreatedAt { get; set; }
        public string Status { get; set; } = null!;
    }
}
