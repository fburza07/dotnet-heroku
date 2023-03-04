using System;
using System.Collections.Generic;


public partial class JournalEntry
{
    public long Id { get; set; }

    public long? Execution { get; set; }

    public byte[]? Text { get; set; }

    public long? Time { get; set; }

    public virtual Execution? ExecutionNavigation { get; set; }
}
