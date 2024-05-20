using Microsoft.Extensions.Options;

namespace RecipesAPI.Interceptor
{
    
    public class LoggingOptions
    {
        public bool EnableLogging { get; set; }
    }

    public static class ServiceConfiguration
    {
        public static void ConfigureLoggingOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<LoggingOptions>(configuration.GetSection("LoggingOptions"));
        }
    }

}
