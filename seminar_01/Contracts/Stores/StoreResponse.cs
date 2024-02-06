namespace Contracts.Stores;

public sealed class StoreResponse
{
    public int Id { get; set; }
    public required string IdentityNumber { get; set; }
}