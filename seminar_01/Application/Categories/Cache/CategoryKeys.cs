namespace Application.Categories.Cache;

public static class CategoryKeys
{
    public static string All() => "CategoryKeys.All";
    public static string Id(int id) => $"Category.{id}";
}
