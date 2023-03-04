using System;
using System.Collections.Generic;


public partial class Position
{
    public long Account { get; set; }

    public long Instrument { get; set; }

    public double? AvgPrice { get; set; }

    public long? MarketPosition { get; set; }

    public long? Quantity { get; set; }

    public long? StatementDate { get; set; }

    public virtual Account AccountNavigation { get; set; } = null!;

    public virtual Instrument InstrumentNavigation { get; set; } = null!;
}
