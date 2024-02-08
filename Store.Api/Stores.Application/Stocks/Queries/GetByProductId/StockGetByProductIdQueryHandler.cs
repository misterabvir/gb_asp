using StoreApplication.Abstractions;
using StoreApplication.Base;
using StoreApplication.Stocks.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StorePersistence.Contexts;
using System.Net.Http.Json;
using StoreApplication.Stocks.Errors;
using Stores.Application.Helpers;

namespace StoreApplication.Stocks.Queries.GetByProductId;

public sealed class StockGetByProductIdQueryHandler : IQueryHandler<StockGetByProductIdQuery, Result<IEnumerable<StockResultResponse>, Error>>
{

    private readonly StoreContext _context;
    private readonly IMapper _mapper;
    private readonly HttpClient _client;
    private readonly IExternalApiLinks _links;

    public StockGetByProductIdQueryHandler(StoreContext context, IMapper mapper, HttpClient client, IExternalApiLinks links)
    {
        _context = context;
        _mapper = mapper;
        _client = client;
        _links = links;
    }

    public async Task<Result<IEnumerable<StockResultResponse>, Error>> Handle(StockGetByProductIdQuery request, CancellationToken cancellationToken)
    {
        var data = await _client.GetAsync(_links.ProductIsExistLink + request.ProductId, cancellationToken);
        var result = await data.Content.ReadFromJsonAsync<bool>(cancellationToken);
        if (!result)
        {
            return StockErrors.ProductNotExist(request.ProductId);
        }

        var stocks = await _context.Stocks.Where(s => s.ProductId == request.ProductId).ToListAsync(cancellationToken: cancellationToken);

        return stocks.Select(s => _mapper.Map<StockResultResponse>(s)!).ToList();
    }
}
