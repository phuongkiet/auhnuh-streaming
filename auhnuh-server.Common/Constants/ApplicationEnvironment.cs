namespace auhnuh_server.Common.Constants
{
    public static class ApplicationEnvironment
    {
        public const string Development = "Development";
        public const string Staging = "Staging";
        public const string Production = "Production";

        private static string? _environment;

        public static void SetEnvironment(string environment)
        {
            _environment = environment;
        }

        public static string GetEnvironment()
        {
            return _environment ??= Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";
        }

        public static bool IsDevelopment()
        {
            return GetEnvironment() == Development;
        }
    }
}
