using Polly;
using Polly.Extensions.Http;

namespace Api;

public static class Extensions
{
    public static void AddWeatherDependencies(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddHttpClient<IWeatherService, WeatherService>(client =>
            {
                var url = builder.Configuration.GetValue<string>("WeatherApi:BaseUrl");
                client.BaseAddress = new Uri(url);
            })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(GetRetryPolicy());
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        const int maxRetry = 3;
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(maxRetry, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }
}