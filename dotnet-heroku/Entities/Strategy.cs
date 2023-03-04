using System;
using System.Collections.Generic;


public partial class Strategy
{
    public long Id { get; set; }

    public long? Category { get; set; }

    public string? Classname { get; set; }

    public long? IsReplay { get; set; }

    public long? IsResetOnNewTradingDay { get; set; }

    public long? IsTerminal { get; set; }

    public string? Name { get; set; }

    public long? ServerId { get; set; }

    public string? Template { get; set; }

    public byte[]? Userdata { get; set; }

    public string? Workspace { get; set; }

    public virtual ICollection<Strategy2Account> Strategy2Accounts { get; } = new List<Strategy2Account>();

    public virtual ICollection<Strategy2Instrument> Strategy2Instruments { get; } = new List<Strategy2Instrument>();

    public virtual ICollection<Execution> Executions { get; } = new List<Execution>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
