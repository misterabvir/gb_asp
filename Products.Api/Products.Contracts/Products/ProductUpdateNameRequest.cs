using System.Text.Json.Serialization;

namespace ProductContracts.Products;

public class ProductUpdateNameRequest
{
    [JsonPropertyName("product_id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}

