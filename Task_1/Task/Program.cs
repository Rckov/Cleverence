using System;
using System.Text;

internal class Program
{
    private static void Main()
    {
        var value = "aaabbcccdde";

        var compressed = StringCompressor.Compress(value);
        var decompressed = StringCompressor.Decompress(compressed);

        Console.WriteLine($"Строка: {value}");
        Console.WriteLine($"Компрессия: {compressed}");
        Console.WriteLine($"Декомпрессия: {decompressed}");
    }
}

public static class StringCompressor
{
    public static string Compress(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        var span = value.AsSpan();
        var result = new StringBuilder(value.Length);

        var count = 0;
        var currentChar = span[0];

        for (var index = 0; index < span.Length; index++)
        {
            if (span[index] == currentChar)
            {
                count++;
            }
            else
            {
                result.Append(currentChar);
                if (count > 1)
                {
                    result.Append(count);
                }

                currentChar = span[index];
                count = 1;
            }
        }

        result.Append(currentChar);
        if (count > 1)
        {
            result.Append(count);
        }

        return result.ToString();
    }

    public static string Decompress(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        var index = 0;
        var result = new StringBuilder(value.Length * 2);

        while (index < value.Length)
        {
            var currentChar = value[index];
            index++;

            var count = 0;
            while (index < value.Length && char.IsDigit(value[index]))
            {
                count = count * 10 + (value[index] - '0');
                index++;
            }

            result.Append(currentChar, count > 0 ? count : 1);
        }

        return result.ToString();
    }
}