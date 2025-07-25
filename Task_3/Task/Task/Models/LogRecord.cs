using System;

using Task_3.Extensions;

namespace Task_3.Models;

public sealed record LogRecord(DateTime Date, DateTime Time, string LogLevel, string? Method, string Message)
{
    public override string ToString()
    {
        var logLevel = LogLevel.NormalizeLogLevel();
        var method = Method is null ? "DEFAULT" : Method;

        return $"{Date:dd-MM-yyyy}\t{Time:HH:mm:ss.fff}\t{logLevel}\t{method}\t{Message}";
    }
}