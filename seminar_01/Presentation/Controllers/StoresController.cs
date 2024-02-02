using Contracts;
using Contracts.Stores;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories.Abstractions;
using Presentation.Extensions;

namespace Presentation.Controllers;

public class StoresController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStoreRepository _storeRepository;
    private readonly IStockRepository _stockRepository;

    public StoresController(
        IUnitOfWork unitOfWork,
        IStoreRepository storeRepository,
        IStockRepository stockRepository)
    {
        _unitOfWork = unitOfWork;
        _storeRepository = storeRepository;
        _stockRepository = stockRepository;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _storeRepository.Get();
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!.ToResponse());
    }

    [HttpGet]
    [Route("by/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _storeRepository.Get(id);
        if (result.IsFailure)
        {           
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!.ToResponse());
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(StoreCreateRequest request)
    {
        var result = await _storeRepository.Create(request.ToEntity());
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(result.Value!.ToResponse());
    }

    [HttpPut]
    [Route("update/identity")]
    public async Task<IActionResult> UpdateIdentityNumber(StoreUpdateIdentityNumberRequest request)
    {
        var result = await _storeRepository.UpdateIdentityNumber(request.Id, request.IdentityNumber);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(result.Value!.ToResponse());
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete(StoreDeleteRequest request)
    {
        var result = await _storeRepository.Delete(request.Id);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(result.Value!.ToResponse());
    }

    [HttpPut]
    [Route("import/product")]
    public async Task<IActionResult> ImportProductToStore(StockImportRequest request)
    {
        var result = await _stockRepository.ImportProduct(request.ProductId, request.StoreId, request.Quantity);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(result.Value!.ToResponse());
    }

    [HttpPut]
    [Route("export/product")]
    public async Task<IActionResult> ExportProductFromStore(StockExportRequest request)
    {
        var result = await _stockRepository.ExportProduct(request.ProductId, request.StoreId, request.Quantity);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(result.Value!.ToResponse());
    }

    [HttpPut]
    [Route("relocate/product")]
    public async Task<IActionResult> RelocateProductBetweenStores(StockRelocateRequest request)
    {
        var result = await _stockRepository.RelocateProduct(request.ProductId, request.FromStoreId, request.ToStoreId, request.Quantity);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(result.Value!.ToResponse());
    }
}
