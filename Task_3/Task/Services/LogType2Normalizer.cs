using System;
using System.Text.RegularExpressions;

using Task_3.Models;
using Task_3.Services.Interfaces;

namespace Task_3.Services;

internal partial class LogType2Normalizer : ILogNormalizer
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
                match.Groups["Method"].Value,
                match.Groups["Message"].Value
            );
        }

        return null;
    }

    [GeneratedRegex(@"^(?<Date>\d{4}-\d{2}-\d{2})\s+(?<Time>\d{2}:\d{2}:\d{2}\.\d+)\|\s*(?<LogLevel>\S+)\|\d+\|(?<Method>[^|]+)\|\s*(?<Message>.+)$", RegexOptions.Compiled)]
    private static partial Regex Pattern();
}