using System;
using System.Collections.Generic;


public partial class User2Account
{
    public long Account { get; set; }

    public long User { get; set; }

    public long? IsViewOnly { get; set; }

    public virtual Account AccountNavigation { get; set; } = null!;

    public virtual User UserNavigation { get; set; } = null!;
}
