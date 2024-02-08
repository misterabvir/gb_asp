using StoreApplication.Stocks.Commands.ExportFromStore;
using StoreApplication.Stocks.Commands.ImportToStore;
using StoreApplication.Stocks.Commands.MoveBetweenStores;
using StoreApplication.Stocks.Queries.GetByProductId;
using StoreApplication.Stocks.Queries.GetByStoreId;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using StoreContracts.Stocks;

namespace StorePresentation.Controllers;

public class StockController : BaseController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public StockController(
        ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }


    [HttpGet]
    [Route("get_by_product_id")]
    public async Task<IActionResult> GetStockByProductId([FromQuery] StockByProductIdRequest request)
    {
        var query = _mapper.Map<StockGetByProductIdQuery>(request)!;
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!.Select(_mapper.Map<StockResponse>));
    }

    [HttpGet]
    [Route("is_product_contains_in_stocks")]
    public async Task<IActionResult> IsProductContainsInStocks([FromQuery] StockByProductIdRequest request)
    {
        var query = _mapper.Map<StockGetByProductIdQuery>(request)!;
        var result = await _sender.Send(query);
        if (result.IsFailure || !result.Value!.Any())
        {
            return Ok(false);
        }
        return Ok(true);
    }

    [HttpGet]
    [Route("get_by_store_id")]
    public async Task<IActionResult> GetStockByStoreId([FromQuery] StockByStoreIdRequest request)
    {
        var query = _mapper.Map<StockGetByStoreIdQuery>(request)!;
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!.Select(_mapper.Map<StockResponse>));
    }


    [HttpPut]
    [Route("import_product_to_store")]
    public async Task<IActionResult> ImportProductToStore(StockImportRequest request)
    {
        var command = _mapper.Map<StockImportToStoreCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<StockResponse>(result.Value!));
    }

    [HttpPut]
    [Route("export_product_from_store")]
    public async Task<IActionResult> ExportProductFromStore(StockExportRequest request)
    {
        var command = _mapper.Map<StockExportFromStoreCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<StockResponse>(result.Value!));
    }

    [HttpPut]
    [Route("move_product_between_stores")]
    public async Task<IActionResult> MoveProductBetweenStores(StockMoveRequest request)
    {
        var command = _mapper.Map<StockMoveBetweenStoresCommand>(request)!;
        var result = await _sender.Send(command)!;

        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!.Select(_mapper.Map<StockResponse>));
    }
}

