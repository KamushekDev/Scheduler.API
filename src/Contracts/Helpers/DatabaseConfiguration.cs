namespace Contracts.Helpers
{
    public static class DatabaseConfiguration
    {
        public static string ConnectionString =
            "User ID=postgres;Password=postgres;Host=31.134.151.83;Port=5433;Database=scheduler;Pooling=true;";
        public static int TimeoutSeconds = 30;
    }
}