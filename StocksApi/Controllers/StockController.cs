using Contracts.Stocks.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StocksApi.BusinessLogicalLayer.Services.Base;

namespace StocksApi.Controllers;

[Route("stocks-api/stocks")]
[ApiController]
public class StockController : ControllerBase
{
    private readonly IStockService _stockService;

    public StockController(IStockService stockService)
    {
        _stockService = stockService;
    }

    [Authorize]
    [HttpGet(template:"get_by_product_id")]
    public async Task<IActionResult> GetStocksByProductId([FromQuery]StockGetByProductIdRequest request)
    {
        var response = await _stockService.GetStocksByProductId(request);
        return Ok(response);
    }

    [Authorize]
    [HttpGet(template: "get_by_store_id")]
    public async Task<IActionResult> GetStocksByStoreId([FromQuery] StockGetByStoreIdRequest request)
    {
        var response = await _stockService.GetStocksByStoreId(request);
        return Ok(response);
    }
    [AllowAnonymous]
    [HttpGet(template: "existing_by_product_id")]
    public async Task<bool> ExistingStocksByProductId([FromQuery] StockIsExistByProductIdRequest request)
    {
        return await _stockService.IsExistStocksByProductId(request);
    }
    [AllowAnonymous]
    [HttpGet(template: "existing_by_store_id")]
    public async Task<bool> ExistingStocksByStoreId([FromQuery] StockIsExistByStoreIdRequest request)
    {
        return await _stockService.IsExistStocksByStoreId(request);
    }

    [Authorize]
    [HttpPut(template: "export_from_store")]
    public async Task<IResult> ExportFromStore(StockExportFromStoreRequest request)
    {
        var response = await _stockService.ExportFromStore(request);
        return response;
    }

    [Authorize]
    [HttpPut(template: "import_to_store")]
    public async Task<IResult> ImportToStore(StockImportToStoreRequest request)
    {
        var response = await _stockService.ImportToStore(request);
        return response;
    }

    [Authorize]
    [HttpPut(template: "exchange_between_stores")]
    public async Task<IResult> ExchangeBetweenStores(StockExchangeBetweenStoresRequest request)
    {
        var response = await _stockService.ExchangeBetweenStores(request);
        return response;
    }

}