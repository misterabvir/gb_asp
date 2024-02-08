using System.Text.Json.Serialization;

namespace ProductContracts.Categories;

public class CategoryCreateRequest
{
    [JsonPropertyName("name")] 
    public required string Name { get; set; }
}
