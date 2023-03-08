using Alba;

namespace Tests;

public class IntegrationTests : IClassFixture<WebApiTestFixture>
{
    private readonly IAlbaHost _host;
    
    public IntegrationTests(WebApiTestFixture app)
    {
        _host = app.Host;
    }
    
    [Fact]
    public async Task Should_Get_Healthy_Response()
    {
        await _host.Scenario(_ =>
        {
            _.Get.Url("/api/health");
            _.ContentShouldBe("Healthy");
            _.StatusCodeShouldBeOk();
        });
    }
    
    [Theory]
    [InlineData("paris")]
    [InlineData("dakar")]
    public async Task Should_Get_Weather_Response(string city)
    {
        await _host.Scenario(_ =>
        {
            _.Get.Url($"/api/weather/{city}");
            _.StatusCodeShouldBeOk();
        });
    }
}