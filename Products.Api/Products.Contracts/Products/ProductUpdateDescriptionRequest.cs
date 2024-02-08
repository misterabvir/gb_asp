using System.Text.Json.Serialization;

namespace ProductContracts.Products;

public class ProductUpdateDescriptionRequest
{
    [JsonPropertyName("product_id")]
    public Guid Id { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; } = string.Empty;
}
