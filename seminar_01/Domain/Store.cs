using Domain.Base;

namespace Domain;

public class  Store : Entity
{
    public int Id { get; set; }
    public required string IdentityNumber { get; set; }
    public List<Stock>? Stocks { get; set; }
}
