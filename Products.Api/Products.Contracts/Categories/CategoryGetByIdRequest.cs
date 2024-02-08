using System.Text.Json.Serialization;

namespace ProductContracts.Categories;

public record CategoryGetByIdRequest
{
    [JsonPropertyName("category_id")]
    public Guid Id { get; set; }
}


