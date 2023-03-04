using System;
using System.Collections.Generic;


public partial class Account
{
    public long Id { get; set; }

    public long? Denomination { get; set; }

    public long? ForexLotSize { get; set; }

    public string? Name { get; set; }

    public long? Provider { get; set; }

    public long? SimulatorDelayExchange { get; set; }

    public long? SimulatorDelayInternet { get; set; }

    public double? SimulatorInitialCash { get; set; }

    public string? Commission { get; set; }

    public string? DisplayName { get; set; }

    public string? Fcm { get; set; }

    public string? Risk { get; set; }

    public double? MinimumCashValue { get; set; }

    public long? PrimaryUser { get; set; }

    public long? AccountStatus { get; set; }

    public double? LossLimit { get; set; }

    public string? Fee { get; set; }

    public double? MarginMultiplier { get; set; }

    public long? MaxOrderSize { get; set; }

    public long? MaxPositionSize { get; set; }

    public byte[]? Data { get; set; }

    public long? LiquidationState { get; set; }

    public virtual ICollection<AccountItem> AccountItems { get; } = new List<AccountItem>();

    public virtual ICollection<Execution> Executions { get; } = new List<Execution>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Strategy2Account> Strategy2Accounts { get; } = new List<Strategy2Account>();
}
