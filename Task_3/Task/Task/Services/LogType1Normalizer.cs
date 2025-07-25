using System;
using System.Text.RegularExpressions;

using Task_3.Models;
using Task_3.Services.Interfaces;

namespace Task_3.Services;

internal partial class LogType1Normalizer : ILogNormalizer
{
    public bool TryMatch(string line) => Pattern().IsMatch(line);

    public LogRecord? Normalize(string value)
    {
        var match = Pattern().Match(value);

        if (match.Success)
        {
            return new LogRecord(
                DateTime.Parse(match.Groups["Date"].Value),
                DateTime.Parse(match.Groups["Time"].Value),
                match.Groups["LogLevel"].Value,
                null,
                match.Groups["Message"].Value
            );
        }

        return null;
    }

    [GeneratedRegex(@"^(?<Date>\d{2}\.\d{2}\.\d{4})\s+(?<Time>\d{2}:\d{2}:\d{2}\.\d{3})\s+(?<LogLevel>\S+)\s+(?<Message>.+)$", RegexOptions.Compiled)]
    private static partial Regex Pattern();
}