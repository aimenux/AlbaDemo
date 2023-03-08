using Api;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.AddWeatherDependencies();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.MapGet("/api/health", () => "Healthy").WithName("GetHealthInfo");

app.MapGet("/api/weather/{city}",
    async (string city, IWeatherService weatherService, CancellationToken cancellationToken) =>
    {
        var info = await weatherService.GetWeatherInfoAsync(city, cancellationToken);
        return info is null 
            ? Results.NotFound($"Weather not found for city {city}") 
            : Results.Ok(info);
    }).WithName("GetWeatherInfo");

app.Run();