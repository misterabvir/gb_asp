using System.Text.Json.Serialization;

namespace ProductContracts.Products;

public class ProductDeleteRequest
{
    [JsonPropertyName("product_id")] 
    public Guid Id { get; set; }
}

