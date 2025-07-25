using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Task_3.Models;
using Task_3.Services;
using Task_3.Services.Interfaces;

internal class Program
{
    private const int BUFFER_SIZE = 1024 * 1024;

    private const string d = "problems.txt";
    private const string s = "logfile_normalized.log";

    private static async Task Main(string[] args)
    {
        var pathfile = "logfile.log";
        var processor = new LogProcessor();

        if (!File.Exists(pathfile))
        {
            throw new FileNotFoundException(pathfile);
        }

        await Process(pathfile, new LogProcessor());
    }

    private static async Task Process(string file, LogProcessor processor)
    {
        await using var writer = new StreamWriter(s, false);
        await using var errors = new StreamWriter(d, false);

        using var readerLog = new StreamReader(new BufferedStream(File.OpenRead(file), BUFFER_SIZE));

        while (!readerLog.EndOfStream)
        {
            var line = await readerLog.ReadLineAsync();

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var record = processor.Normalize(line);

            if (record is null)
            {
                await errors.WriteLineAsync(line);
            }
            else
            {
                await writer.WriteLineAsync(record.ToString());
            }
        }
    }
}

public class LogProcessor()
{
    private readonly List<ILogNormalizer> _normalizers =
    [
        new LogType1Normalizer(),
        new LogType2Normalizer()
    ];

    public LogRecord? Normalize(string value)
    {
        var normalizer = GetNormalizer(value);

        try
        {
            return normalizer?.Normalize(value);
        }
        catch
        {
            return null;
        }
    }

    private ILogNormalizer? GetNormalizer(string value)
    {
        return _normalizers.FirstOrDefault(x => x.TryMatch(value));
    }
}