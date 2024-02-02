using Domain.Base;

namespace Domain;

public class Product : Entity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }     
    public decimal Price { get; set; }  
    public List<Stock>? Stocks { get; set; }

    public virtual int? CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}