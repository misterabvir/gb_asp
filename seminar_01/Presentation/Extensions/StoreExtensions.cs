using Contracts.Stores;
using Domain;

namespace Presentation.Extensions;

public static class StoreExtensions
{
    public static StoreResponse ToResponse(
        this Store store)
        => new(store.Id, store.IdentityNumber);

    public static IEnumerable<StoreResponse> ToResponse(
        this IEnumerable<Store> stores)
        => stores.Select(ToResponse);

    public static Store ToEntity(
        this StoreCreateRequest request)
        => new() { IdentityNumber = request.IdentityNumber };
}