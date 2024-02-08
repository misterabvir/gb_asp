using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stocks.Commands.ExportFromStore;
using StoreApplication.Stocks.Errors;
using StoreApplication.Stocks.Responses;
using StoreApplication.Stores.Errors;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StorePersistence.Contexts;
using System.Net.Http.Json;
using Stores.Application.Helpers;

namespace StoreApplication.Stocks.Commands.MoveBetweenStores;

public sealed class StockMoveBetweenStoresCommandHandler : ICommandHandler<StockMoveBetweenStoresCommand, Result<IEnumerable<StockResultResponse>, Error>>
{

    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly HttpClient _client;
    private readonly IExternalApiLinks _links;

    public StockMoveBetweenStoresCommandHandler(StoreContext context, IMapper mapper, HttpClient client, IExternalApiLinks links)
    {
        _context = context;
        _mapper = mapper;
        _client = client;
        _links = links;
    }

    public async Task<Result<IEnumerable<StockResultResponse>, Error>> Handle(StockMoveBetweenStoresCommand request, CancellationToken cancellationToken)
    {
        var data = await _client.GetAsync(_links.ProductIsExistLink + request.ProductId, cancellationToken);
        var result = await data.Content.ReadFromJsonAsync<bool>(cancellationToken);
        if (!result)
        {
            return StockErrors.ProductNotExist(request.ProductId);
        }

        var sender = await _context.Stores
            .FirstOrDefaultAsync(s => 
            s.Id == request.SenderStoreId, 
            cancellationToken: cancellationToken);
        if (sender == null)
        {
            return StoreErrors.NotFound(request.SenderStoreId);
        }

        var target = await _context.Stores
            .FirstOrDefaultAsync(s => 
            s.Id == request.TargetStoreId, 
            cancellationToken: cancellationToken);
        if (target == null)
        {
            return StoreErrors.NotFound(request.TargetStoreId);
        }

        var senderStock = await _context.Stocks
            .FirstOrDefaultAsync(sp => 
            sp.ProductId == request.ProductId && sp.StoreId == request.SenderStoreId, 
            cancellationToken: cancellationToken);
        if (senderStock is null)
        {
            return new NotFound(nameof(StockExportFromStoreCommandHandler), $"Stock for product with id {request.ProductId} not found in sender with id {request.SenderStoreId}");
        }

        if (senderStock.Quantity < request.Quantity)
        {
            return StockErrors.NotEnoughQuantity(request.ProductId, request.SenderStoreId, request.Quantity);
        }

        var targetStock = await _context.Stocks
           .FirstOrDefaultAsync(sp => 
           sp.ProductId == request.ProductId && sp.StoreId == request.TargetStoreId, 
           cancellationToken: cancellationToken);
        if (targetStock is null)
        {
            targetStock = new() { ProductId = request.ProductId, StoreId = request.TargetStoreId, Quantity = 0 };
            await _context.Stocks.AddAsync(targetStock, cancellationToken);
        }

        senderStock.Quantity -= request.Quantity;
        targetStock.Quantity += request.Quantity;

        if (targetStock.Quantity == 0)
        {
            _context.Stocks.Remove(targetStock);
        }

        await _context.SaveChangesAsync(cancellationToken);

        return new List<StockResultResponse> { _mapper.Map<StockResultResponse>(senderStock)!, _mapper.Map<StockResultResponse>(targetStock)! };

    }
}
