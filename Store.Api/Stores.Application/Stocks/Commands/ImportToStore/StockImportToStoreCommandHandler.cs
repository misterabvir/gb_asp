using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stocks.Errors;
using StoreApplication.Stocks.Responses;
using StoreApplication.Stores.Errors;
using AutoMapper;
using StoreDomain;
using Microsoft.EntityFrameworkCore;
using StorePersistence.Contexts;
using System.Net.Http.Json;
using Stores.Application.Helpers;

namespace StoreApplication.Stocks.Commands.ImportToStore;

public sealed class StockImportToStoreCommandHandler : ICommandHandler<StockImportToStoreCommand, Result<StockResultResponse, Error>>
{
    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly HttpClient _client;
    private readonly IExternalApiLinks _links;

    public StockImportToStoreCommandHandler(StoreContext context, IMapper mapper, HttpClient client, IExternalApiLinks links)
    {
        _context = context;
        _mapper = mapper;
        _client = client;
        _links = links;
    }

    public async Task<Result<StockResultResponse, Error>> Handle(StockImportToStoreCommand request, CancellationToken cancellationToken)
    {              
        if (request.Quantity <= 0)
        {
            return StockErrors.QuantityMustBePositive(request.Quantity);
        }

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

        var stock = await _context.Stocks.FirstOrDefaultAsync(sp => sp.ProductId == request.ProductId && sp.StoreId == sp.StoreId, cancellationToken: cancellationToken);
        if (stock is null)
        {
            stock = _mapper.Map<Stock>(request)!;
            await _context.Stocks.AddAsync(stock, cancellationToken);
        }
        else
        {
            stock.Quantity += request.Quantity;
        }
        
        await _context.SaveChangesAsync(cancellationToken);

        return _mapper.Map<StockResultResponse>(stock)!;

    }
}
