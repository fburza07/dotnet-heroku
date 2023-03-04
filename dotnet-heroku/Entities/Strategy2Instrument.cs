using System;
using System.Collections.Generic;


public partial class Strategy2Instrument
{
    public long Instrument { get; set; }

    public long Strategy { get; set; }

    public long Nr { get; set; }

    public virtual Instrument InstrumentNavigation { get; set; } = null!;

    public virtual Strategy StrategyNavigation { get; set; } = null!;
}
