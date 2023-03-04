using System;
using System.Collections.Generic;


public partial class Execution
{
    public long Id { get; set; }

    public long Account { get; set; }

    public long? BarIndex { get; set; }

    public double? Commission { get; set; }

    public long? Exchange { get; set; }

    public string ExecutionId { get; set; } = null!;

    public double? Fee { get; set; }

    public long Instrument { get; set; }

    public long? IsEntry { get; set; }

    public long? IsEntryStrategy { get; set; }

    public long? IsExit { get; set; }

    public long? IsExitStrategy { get; set; }

    public long? LotSize { get; set; }

    public long? MarketPosition { get; set; }

    public double? MaxPrice { get; set; }

    public double? MinPrice { get; set; }

    public string? Name { get; set; }

    public string? OrderId { get; set; }

    public long? Position { get; set; }

    public long? PositionStrategy { get; set; }

    public double? Price { get; set; }

    public long? Quantity { get; set; }

    public double? Rate { get; set; }

    public long? StatementDate { get; set; }

    public long? Time { get; set; }

    public string? ServerName { get; set; }

    public virtual Account AccountNavigation { get; set; } = null!;

    public virtual Instrument InstrumentNavigation { get; set; } = null!;

    public virtual ICollection<JournalEntry> JournalEntries { get; } = new List<JournalEntry>();

    public virtual ICollection<Strategy> Strategies { get; } = new List<Strategy>();
}
