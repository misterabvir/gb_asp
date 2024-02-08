using System.Text.Json.Serialization;

namespace ProductContracts.Categories;

public class CategoryDeleteRequest
{
    [JsonPropertyName("category_id")]
    public Guid Id { get; set; }
}
