using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stocks.Errors;
using StoreApplication.Stocks.Responses;
using StoreApplication.Stores.Errors;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StorePersistence.Contexts;
using System.Net.Http.Json;
using Stores.Application.Helpers;

namespace StoreApplication.Stocks.Commands.ExportFromStore;

public sealed class StockExportFromStoreCommandHandler : ICommandHandler<StockExportFromStoreCommand, Result<StockResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly HttpClient _client;
    private readonly IExternalApiLinks _links;

    public StockExportFromStoreCommandHandler(StoreContext context, IMapper mapper, HttpClient client, IExternalApiLinks links)
    {
        _context = context;
        _mapper = mapper;
        _client = client;
        _links = links;
    }

    public async Task<Result<StockResultResponse, Error>> Handle(StockExportFromStoreCommand request, CancellationToken cancellationToken)
    {
        var data = await _client.GetAsync(_links.ProductIsExistLink + request.ProductId, cancellationToken);
        var result = await data.Content.ReadFromJsonAsync<bool>(cancellationToken);
        if (!result)
        {
            return StockErrors.ProductNotExist(request.ProductId);
        }

        var store = await _context.Stores.FirstOrDefaultAsync(s => s.Id == request.StoreId, cancellationToken: cancellationToken);
        if (store == null)
        {
            return StoreErrors.NotFound(request.StoreId);
        }

        var stock = await _context.Stocks
            .FirstOrDefaultAsync(sp =>
            sp.ProductId == request.ProductId &&
            sp.StoreId == request.StoreId,
            cancellationToken: cancellationToken);
        if (stock is null)
        {
            return StockErrors.NotFound(request.ProductId, request.StoreId);
        }

        if (stock.Quantity < request.Quantity)
        {
            return StockErrors.NotEnoughQuantity(request.ProductId, request.StoreId, stock.Quantity);
        }

        stock.Quantity -= request.Quantity;

        if (stock.Quantity == 0)
        {
            _context.Stocks.Remove(stock);
        }
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<StockResultResponse>(stock)!;
    }
}
