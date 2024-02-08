using System.Text.Json.Serialization;

namespace StoreContracts.Stocks;

public record StockByProductIdRequest
{
    [JsonPropertyName("product_id")] 
    public Guid ProductId { get; set; }
}
