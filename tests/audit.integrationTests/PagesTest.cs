using System.Threading.Tasks;
using System;
using Xunit;
using Microsoft.AspNetCore.Mvc.Testing;

namespace audit.integrationTests;

public class PagesTest  : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    public PagesTest(WebApplicationFactory<Program> factory)
    {
        ArgumentNullException.ThrowIfNull(factory);
        _factory = factory;
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/Index")]
    [InlineData("/Privacy")]
    // [InlineData("/DiffSequence")] needs id
    // [InlineData("/Sequence")] needs id
    public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    {
        // arrange
        var client = _factory.CreateClient();

        // act
        var response = await client.GetAsync(url);

        // assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("text/html; charset=utf-8", 
            response.Content.Headers.ContentType.ToString());
    }
}