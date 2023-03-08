using Alba;

namespace Tests;

public class WebApiTestFixture : IAsyncLifetime
{
    public IAlbaHost Host { get; private set; }

    public async Task InitializeAsync()
    {
        Host = await AlbaHost.For<Program>(builder =>
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
            });

            builder.ConfigureServices((context, services) =>
            {
            });
        });
    }

    public async Task DisposeAsync()
    {
        await Host.DisposeAsync();
    }
}