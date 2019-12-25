namespace Contracts.Helpers
{
    public static class DatabaseConfiguration
    {
        public static string ConnectionString =
            "User ID=postgres;Password=postgres;Server=localhost:5433;Database=scheduler;Pooling=true;";
                                                //jdbc:postgresql://localhost:5433/postgres
        public static int TimeoutSeconds = 20;
    }
}