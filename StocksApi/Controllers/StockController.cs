using Contracts.Stocks.Requests;
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

    [HttpGet(template:"get_by_product_id")]
    public async Task<IActionResult> GetStocksByProductId([FromQuery]StockGetByProductIdRequest request)
    {
        var response = await _stockService.GetStocksByProductId(request);
        return Ok(response);
    }

    [HttpGet(template: "get_by_store_id")]
    public async Task<IActionResult> GetStocksByStoreId([FromQuery] StockGetByStoreIdRequest request)
    {
        var response = await _stockService.GetStocksByStoreId(request);
        return Ok(response);
    }

    [HttpGet(template: "existing_by_product_id")]
    public async Task<IActionResult> ExistingStocksByProductId([FromQuery] StockGetByProductIdRequest request)
    {
        var response = await _stockService.GetStocksByProductId(request);
        return Ok(response is not null && response.Any(s=>s.ProductId == request.ProductId));
    }

    [HttpGet(template: "existing_by_store_id")]
    public async Task<IActionResult> ExistingStocksByStoreId([FromQuery] StockGetByStoreIdRequest request)
    {
        var response = await _stockService.GetStocksByStoreId(request);
        return Ok(response is not null && response.Any(s=>s.StoreId == request.StoreId));
    }

    [HttpPut(template: "export_from_store")]
    public async Task<IActionResult> ExportFromStore(StockExportFromStoreRequest request)
    {
        var response = await _stockService.ExportFromStore(request);
        return response ? Ok("Success") : BadRequest("Failed");
    }

    [HttpPut(template: "import_to_store")]
    public async Task<IActionResult> ImportToStore(StockImportToStoreRequest request)
    {
        var response = await _stockService.ImportToStore(request);
        return response ? Ok("Success") : BadRequest("Failed");
    }

    [HttpPut(template: "exchange_between_stores")]
    public async Task<IActionResult> ExchangeBetweenStores(StockExchangeBetweenStoresRequest request)
    {
        var response = await _stockService.ExchangeBetweenStores(request);
        return response ? Ok("Success") : BadRequest("Failed");
    }

}