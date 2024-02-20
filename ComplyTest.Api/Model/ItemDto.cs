using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ComplyTest.Core.Entity;

namespace ComplyTest.Api.Model;

public record ItemDto
{
    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; } = string.Empty;

    public Item ToEntity() => new Item { Name = Name };
}
