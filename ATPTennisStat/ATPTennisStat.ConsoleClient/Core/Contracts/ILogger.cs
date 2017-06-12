namespace ATPTennisStat.ConsoleClient.Core.Contracts
{
    public interface ILogger
    {
        void Log(string message);

        void LogDetails(string message);
    }
}
