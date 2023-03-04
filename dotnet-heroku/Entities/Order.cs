using System;
using System.Collections.Generic;


public partial class Order
{
    public long Id { get; set; }
    public long Account { get; set; }
    public double? AvgFillPrice { get; set; }
    public byte[]? Data { get; set; }
    public long? Filled { get; set; }
    public long? Gtd { get; set; }
    public long Instrument { get; set; }
    public string? Name { get; set; }
    public double? LimitPrice { get; set; }
    public string? Oco { get; set; }
    public long? OrderAction { get; set; }
    public long? OrderEntry { get; set; }
    public string? OrderId { get; set; }
    public long? OrderType { get; set; }
    public long? OrderState { get; set; }
    public long? Quantity { get; set; }
    public long? StatementDate { get; set; }
    public double? StopPrice { get; set; }
    public long? Time { get; set; }
    public long? TimeInForce { get; set; }

    public virtual Account AccountNavigation { get; set; } = null!;

    public virtual Instrument InstrumentNavigation { get; set; } = null!;

    public virtual ICollection<OrderUpdate> OrderUpdates { get; } = new List<OrderUpdate>();

    public virtual ICollection<Strategy> Strategies { get; } = new List<Strategy>();
}
