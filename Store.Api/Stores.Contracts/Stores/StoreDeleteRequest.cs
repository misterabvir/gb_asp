using System.Text.Json.Serialization;

namespace StoreContracts.Stores;

public class StoreDeleteRequest
{
    [JsonPropertyName("store_id")] public Guid Id { get; set; }
}
