using Task_3.Models;

namespace Task_3.Services.Interfaces;

internal interface ILogNormalizer
{
    bool TryMatch(string value);

    LogRecord? Normalize(string value);
}