namespace Task_3.Extensions;

public static class LogExtensions
{
    extension(string value)
    {
        public string NormalizeLogLevel()
        {
            return value.Trim() switch
            {
                "DEBUG" => "DEBUG",
                "ERROR" => "ERROR",
                "WARNING" or "WARN" => "WARN",
                "INFORMATION" or "INFO" => "INFO",

                _ => "WARNING"
            };
        }
    }
}