using System;
using System.Collections.Generic;


public partial class MasterInstrument
{
    public long Id { get; set; }

    public long? Currency { get; set; }

    public string? Description { get; set; }

    public long? InstrumentType { get; set; }

    public long? MergePolicy { get; set; }

    public string Name { get; set; } = null!;

    public double? PointValue { get; set; }

    public double? PriceLevel { get; set; }

    public double? TickSize { get; set; }

    public string? TradingHours { get; set; }

    public string? Url { get; set; }

    public byte[]? UserData { get; set; }

    public long? Version { get; set; }

    public long? IsServerSupported { get; set; }

    public long? AutoLiquidation { get; set; }

    public virtual ICollection<Instrument> Instruments { get; } = new List<Instrument>();
}
