namespace Contracts.Helpers
{
    public static class DatabaseConfiguration
    {
        public static string ConnectionString =
            "User ID=postgres;Password=postgres;Host=localhost;Port=5433;Database=scheduler;Pooling=true;";
        public static int TimeoutSeconds = 5;
    }
}