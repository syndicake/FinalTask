using Microsoft.Extensions.Configuration;

namespace CoreLayer
{
    public static class Configuration
    {
        public static string AppUrl { get; private set; } = string.Empty;

        static Configuration()
        {
            Init();
        }

        public static void Init()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            AppUrl = configuration["ApplicationUrl"] ?? string.Empty;
        }
    }
}
