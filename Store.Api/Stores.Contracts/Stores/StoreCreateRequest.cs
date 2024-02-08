using System.Text.Json.Serialization;

namespace StoreContracts.Stores;

public class StoreCreateRequest
{
    [JsonPropertyName("identity_number")] public required string IdentityNumber { get; set; }
}
