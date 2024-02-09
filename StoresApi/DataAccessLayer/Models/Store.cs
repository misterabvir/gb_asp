namespace StoresApi.DataAccessLayer.Models;

public class Store
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
}
