using Contracts.Stores.Requests;
using Microsoft.AspNetCore.Mvc;
using StoresApi.BusinessLogicalLayer.Services.Base;

namespace StoresApi.Controllers;

[Route("stores-api/stores")]
[ApiController]
public class StoreController : ControllerBase
{
    private readonly IStoreService _storeService;

    public StoreController(IStoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpGet(template: "get_all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _storeService.GetStores();
        return Ok(response);
    }

    [HttpGet(template: "get_by_id")]
    public async Task<IActionResult> GetById([FromQuery] StoreGetByIdRequest request)
    {
        var response = await _storeService.GetStoreById(request);
        return Ok(response);
    }

    [HttpGet(template: "existing_by_id")]
    public async Task<bool> ExistingById([FromQuery] StoreIsExistByIdRequest request)
    {
        return await _storeService.IsExistStoreById(request);
    }

    [HttpPost(template: "create")]
    public async Task<IResult> Create(StoreCreateRequest request)
    {
        return await _storeService.CreateStore(request);
    }

    [HttpPut(template: "update_name")]
    public async Task<IResult> UpdateName(StoreUpdateNameRequest request)
    {
        return await _storeService.UpdateStore(request);
    }

    [HttpDelete(template: "delete")]
    public async Task<IResult> Delete(StoreDeleteRequest request)
    {
        return await _storeService.DeleteStore(request);
    }
}