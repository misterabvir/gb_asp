namespace Application.Stores.Cache;

public static class StoreKeys
{
    public static string All() => "StoreKeys.All";
    public static string Id(int id) => $"Store.{id}";
}