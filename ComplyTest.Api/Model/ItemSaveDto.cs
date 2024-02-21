using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ComplyTest.Core.Entity;

namespace ComplyTest.Api.Model;

public record ItemSaveDto
{
    [JsonPropertyName("name")]
    [Required]
    public string Name { get; set; } = string.Empty;

    public Item ToEntity() => new Item { Name = Name };
}
