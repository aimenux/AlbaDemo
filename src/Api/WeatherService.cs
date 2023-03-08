using System.Text.Json.Serialization;

namespace Api;

public interface IWeatherService
{
    Task<WeatherInfo> GetWeatherInfoAsync(string city, CancellationToken cancellationToken = default);
}

public class WeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<WeatherService> _logger;

    public WeatherService(HttpClient httpClient, ILogger<WeatherService> logger)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task<WeatherInfo> GetWeatherInfoAsync(string city, CancellationToken cancellationToken)
    {
        var dto = await _httpClient.GetFromJsonAsync<WeatherDto>(city, cancellationToken);
        if (dto is null) return null;
        return new WeatherInfo
        {
            Temperature = dto.Temperature,
            Description = dto.Description,
            Wind = dto.Wind
        };
    }

    internal class WeatherDto
    {
        [JsonPropertyName("description")]
        public string Description { get; init; }

        [JsonPropertyName("temperature")]
        public string Temperature { get; init; }

        [JsonPropertyName("wind")]
        public string Wind { get; init; }
    }
}