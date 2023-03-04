using System;
using System.Collections.Generic;


public partial class User2MarketDataEntitlement
{
    public long? MarketData { get; set; }

    public string MarketDataEntitlement { get; set; } = null!;

    public long? MarketDepth { get; set; }

    public long User { get; set; }

    public virtual User UserNavigation { get; set; } = null!;
}
