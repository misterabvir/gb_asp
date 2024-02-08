using System.Text.Json.Serialization;

namespace ProductContracts.Products;

public class ProductGetByIdRequest
{
    [JsonPropertyName("product_id")]
    public Guid Id { get; set; }
}

