using System;
using System.Collections.Generic;


public partial class OrderUpdate
{
    public long Order { get; set; }

    public long Nr { get; set; }

    public double? AvgFillPrice { get; set; }

    public string? Comment { get; set; }

    public long? Error { get; set; }

    public long? Filled { get; set; }

    public double? LimitPrice { get; set; }

    public string? OrderId { get; set; }

    public long? OrderState { get; set; }

    public long? Quantity { get; set; }

    public long? StatementDate { get; set; }

    public double? StopPrice { get; set; }

    public long? Time { get; set; }

    public string? ServerName { get; set; }

    public virtual Order OrderNavigation { get; set; } = null!;
}
