using Domain.Base;

namespace Domain;

public class Category : Entity
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Product>? Products { get; set; }
}

