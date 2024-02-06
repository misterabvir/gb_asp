namespace Contracts.Products;

public class ProductResponse
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public string? Description { get; set; }
    public int? CategoryId { get; set; }
}


