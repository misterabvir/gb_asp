namespace ProductsApi.DataAccessLayer.Models;

public sealed class Product
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public required string Name { get; set; }
    public string Description { get; set; } = string.Empty;
    public double Price { get; set; }
    public Guid CategoryId { get; set; }
    public Category? Category { get; set; }
}
