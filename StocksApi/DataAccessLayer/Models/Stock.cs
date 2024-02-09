namespace StocksApi.DataAccessLayer.Models;

public class Stock
{
    public Guid ProductId { get; set; }
    public Guid StoreId { get; set; }
    public int Quantity { get; set; }
}
