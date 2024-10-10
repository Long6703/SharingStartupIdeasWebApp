using System;
using System.Collections.Generic;

namespace SSI.Share.Domain
{
    public partial class SystemConfiguration
    {
        public int ConfigId { get; set; }
        public string ConfigKey { get; set; } = null!;
        public string ConfigValue { get; set; } = null!;
        public DateTime? UpdatedAt { get; set; }
    }
}
