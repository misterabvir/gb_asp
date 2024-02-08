using System.Text.Json.Serialization;

namespace StoreContracts.Stocks;

public class StockExportRequest
{
    [JsonPropertyName("product_id")]public Guid ProductId { get; set; }
    [JsonPropertyName("store_id")]public Guid StoreId { get; set; }
    [JsonPropertyName("quantity_for_export")]public int Quantity { get; set; }
}

