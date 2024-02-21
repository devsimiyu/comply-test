using System.Text.Json.Serialization;

namespace ComplyTest.Api.Model;

public record ItemFactorialDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; } = Guid.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [JsonPropertyName("factorial")]
    public int Factorial { get; set; }
    
    [JsonPropertyName("row")]
    public int Row { get; set; }
}
