using Contracts.Stores.Requests;
using Microsoft.AspNetCore.Authorization;
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

    [AllowAnonymous]
    [HttpGet(template: "get_all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _storeService.GetStores();
        return Ok(response);
    }
    [Authorize]
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

    [Authorize]
    [HttpPost(template: "create")]
    public async Task<IResult> Create(StoreCreateRequest request)
    {
        return await _storeService.CreateStore(request);
    }

    [Authorize]
    [HttpPut(template: "update_name")]
    public async Task<IResult> UpdateName(StoreUpdateNameRequest request)
    {
        return await _storeService.UpdateStore(request);
    }

    [Authorize]
    [HttpDelete(template: "delete")]
    public async Task<IResult> Delete(StoreDeleteRequest request)
    {
        return await _storeService.DeleteStore(request);
    }
}