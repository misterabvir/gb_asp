using System.Text.Json.Serialization;

namespace ProductContracts.Products;

public class ProductGetByCategoryIdRequest
{
    [JsonPropertyName("category_id")] public Guid? CategoryId { get; set; } = null;
}

