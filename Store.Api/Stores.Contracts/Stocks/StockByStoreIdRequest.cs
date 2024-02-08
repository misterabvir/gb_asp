using System.Text.Json.Serialization;

namespace StoreContracts.Stocks;

public class StockByStoreIdRequest
{
    [JsonPropertyName("store_id")]
    public Guid StoreId { get; set; }
}
