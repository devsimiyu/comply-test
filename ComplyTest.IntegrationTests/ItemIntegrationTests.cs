using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Http;

namespace ComplyTest.IntegrationTests;

public class ItemIntegrationTests : IClassFixture<IntegrationTestFixture<Program>>
{
    private readonly HttpClient _client;
    private readonly IntegrationTestFixture<Program> _fixture;

    public ItemIntegrationTests(IntegrationTestFixture<Program> fixture)
    {
        _fixture = fixture;
        _client = fixture.CreateClient();
    }

    [Fact]
    public async Task Get_ItemList_Works()
    {
        var item = _fixture.Items[1];

        Assert.NotNull(item);

        var response = await _client.GetAsync("/api/item");
        var result = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(item.Name, result);
    }
    
    [Fact]
    public async Task Get_ItemDetails_Works()
    {
        var item = _fixture.Items[0];

        Assert.NotNull(item);

        var response = await _client.GetAsync("/api/item/" + item.Id);
        var result = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Contains(item.Name, result);
    }

    [Fact]
    public async Task Post_CreatesItem_Works()
    {
        var body = new { name = "new item" };
        var response = await _client.PostAsJsonAsync("/api/item", body);
        var result = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        Assert.Contains(body.name, result);
    }

    [Fact]
    public async Task Put_UpdatesItem_Works()
    {
        var item = _fixture.Items[2];

        Assert.NotNull(item);

        var body = new { name = "update item" };
        var response = await _client.PutAsJsonAsync("/api/item/" + item.Id, body);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
    
    [Fact]
    public async Task Delete_RemoveItem_Works()
    {
        var item = _fixture.Items[3];

        Assert.NotNull(item);

        var response = await _client.DeleteAsync("/api/item/" + item.Id);

        Assert.True(response.IsSuccessStatusCode);
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}
