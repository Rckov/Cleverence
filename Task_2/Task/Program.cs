using System.Threading;

internal class Program
{
    private static void Main()
    {
        // В связи с применением статического класса Server, невозможно применить IDisposable для
        // контроля неуправляемого ресурса ReaderWriterLockSlim
    }
}

public static class Server
{
    private static readonly ReaderWriterLockSlim _locker = new();
    private static int _count;

    public static int GetCount()
    {
        _locker.EnterReadLock();

        try
        {
            return _count;
        }
        finally
        {
            _locker.ExitReadLock();
        }
    }

    public static void AddToCount(int value)
    {
        _locker.EnterWriteLock();

        try
        {
            _count += value;
        }
        finally
        {
            _locker.ExitWriteLock();
        }
    }
}