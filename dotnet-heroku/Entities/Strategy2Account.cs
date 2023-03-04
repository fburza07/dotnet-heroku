using System;
using System.Collections.Generic;


public partial class Strategy2Account
{
    public long Account { get; set; }

    public long Strategy { get; set; }

    public long Nr { get; set; }

    public virtual Account AccountNavigation { get; set; } = null!;

    public virtual Strategy StrategyNavigation { get; set; } = null!;
}
