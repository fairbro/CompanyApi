using CompanyApi.Dtos;
using CompanyApi.Errors;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Json;

namespace CompanyApi.Tests;

public class CompanyApiTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public CompanyApiTests(WebApplicationFactory<Program> factory)
    {
        var configuredFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config.AddJsonFile("appsettings.json")
                      .AddJsonFile("appsettings.Development.json", optional: true);
            });
        });

        _client = configuredFactory.CreateClient();
    }

    [Fact]
    public async Task GetCompany_Returns200_WhenCompanyExists()
    {
        // Arrange
        var id = 1;

        // Act
        var response = await _client.GetAsync($"/companies/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var dto = await response.Content.ReadFromJsonAsync<CompanyDto>();
        Assert.NotNull(dto);
        Assert.Equal(id, dto!.Id);
        Assert.False(string.IsNullOrWhiteSpace(dto.Name));
    }

    [Fact]
    public async Task GetCompany_Returns404_WhenCompanyDoesNotExist()
    {
        // Arrange
        var id = 9999;

        // Act
        var response = await _client.GetAsync($"/companies/{id}");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(error);
        Assert.Equal("Not Found", error!.Error);
        Assert.Contains(id.ToString(), error.ErrorDescription);
    }

    [Fact]
    public async Task GetCompany_Returns404_WhenIdIsInvalid()
    {
        // Act
        var response = await _client.GetAsync("/companies/-1");

        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);

        var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
        Assert.NotNull(error);
        Assert.Equal("Not Found", error!.Error);
    }

    [Fact]
    public async Task Health_Returns200()
    {
        var response = await _client.GetAsync("/health");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
