namespace Task.Test;

public class StringCompressorTests
{
    [Theory]
    [InlineData("a", "a")]
    [InlineData("aa", "a2")]
    [InlineData("aaa", "a3")]
    [InlineData("aab", "a2b")]
    [InlineData("aaabbcccdde", "a3b2c3d2e")]
    [InlineData("abc", "abc")]
    [InlineData("aabbbc", "a2b3c")]
    [InlineData("zzzzzzzzzz", "z10")]
    public void Test1(string value, string compressValue)
    {
        // Act
        var compressed = StringCompressor.Compress(value);
        var decompressed = StringCompressor.Decompress(compressed);

        // Assert
        Assert.Equal(compressValue, compressed);
        Assert.Equal(value, decompressed);
    }
}