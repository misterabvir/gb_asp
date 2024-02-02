using Contracts;
using Domain;

namespace Presentation.Extensions;

public static class StockExtensions
{
    public static StockResponse ToResponse(this Stock stock)
        => new(
             stock.Quantity,
             stock.Product!.ToResponse(),
             stock.Store!.ToResponse());

    public static IEnumerable<StockResponse> ToResponse(this IEnumerable<Stock> stocks)
        => stocks.Select(ToResponse);
}
