using System;
using System.Collections.Generic;


public partial class InstrumentList
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public long? Version { get; set; }

    public long? IsWebOnly { get; set; }

    public virtual ICollection<Instrument> Instruments { get; } = new List<Instrument>();
}
