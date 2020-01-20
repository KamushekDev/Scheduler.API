namespace Contracts.Helpers
{
    public class DatabaseConfiguration
    {
        public string ConnectionString { get; }

        public int TimeoutSeconds { get; }

        public DatabaseConfiguration(string connectionString, int timeoutSeconds = 5)
        {
            ConnectionString = connectionString;
            TimeoutSeconds = timeoutSeconds;
        }
        
    }
}