using System;
using System.Collections.Generic;


public partial class Instrument
{
    public long Id { get; set; }

    public long? Exchange { get; set; }

    public long? Expiry { get; set; }

    public long MasterInstrument { get; set; }

    public long? Right { get; set; }

    public double? StrikePrice { get; set; }

    public virtual ICollection<Execution> Executions { get; } = new List<Execution>();

    public virtual MasterInstrument MasterInstrumentNavigation { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Strategy2Instrument> Strategy2Instruments { get; } = new List<Strategy2Instrument>();

    public virtual ICollection<InstrumentList> InstrumentLists { get; } = new List<InstrumentList>();
}
