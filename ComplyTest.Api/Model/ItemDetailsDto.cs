using System.Text.Json.Serialization;

namespace ComplyTest.Api.Model;

public record ItemDetailsDto
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; } = Guid.Empty;

    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonPropertyName("dateCreated")]
    public DateTime DateCreated { get; set; }

    [JsonPropertyName("dateModified")]
    public DateTime DateModified { get; set; }

    [JsonPropertyName("factorial")]
    public int Factorial { get; set; }
    
    [JsonPropertyName("row")]
    public int Row { get; set; }
}
