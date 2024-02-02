using Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace Domain;

public class Stock : Entity
{
    [Key] public int ProductId { get; set; }
    [Key] public int StoreId { get; set; }

    public virtual Product? Product { get; set; }
    public virtual Store? Store { get; set; }

    public int Quantity { get; set; }
}