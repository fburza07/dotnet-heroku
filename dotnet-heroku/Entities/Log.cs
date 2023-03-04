using System;
using System.Collections.Generic;


public partial class Log
{
    public long Account { get; set; }

    public long? LogCategory { get; set; }

    public long? LogLevel { get; set; }

    public byte[]? Message { get; set; }

    public long? Time { get; set; }

    public long User { get; set; }

    public string? ServerName { get; set; }
}
