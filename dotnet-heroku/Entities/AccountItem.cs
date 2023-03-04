using System;
using System.Collections.Generic;


public partial class AccountItem
{
    public long Account { get; set; }

    public long Currency { get; set; }

    public long ItemType { get; set; }

    public double? Value { get; set; }

    public long? TimeUtc { get; set; }

    public virtual Account AccountNavigation { get; set; } = null!;
}
