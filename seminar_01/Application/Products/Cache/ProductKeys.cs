namespace Application.Products.Cache;

public static class ProductKeys
{
    public static string All() => "ProductKeys.All";
    public static string Id(int id) => $"Product.{id}";
}
