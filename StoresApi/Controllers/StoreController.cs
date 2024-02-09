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
    public async Task<IActionResult> ExistingById([FromQuery] StoreGetByIdRequest request)
    {
        var response = await _storeService.GetStoreById(request);
        return Ok(response is not null);
    }

    [HttpPost(template: "create")]
    public async Task<IActionResult> Create(StoreCreateRequest request)
    {
        var response = await _storeService.CreateStore(request);
        return Ok(response);
    }

    [HttpPut(template: "update_name")]
    public async Task<IActionResult> UpdateName(StoreUpdateNameRequest request)
    {
        var response = await _storeService.UpdateStore(request);
        return response ? Ok("Success") : BadRequest("Fail");
    }

    [HttpDelete(template: "delete")]
    public async Task<IActionResult> Delete(StoreDeleteRequest request)
    {
        var response = await _storeService.DeleteStore(request);
        return response ? Ok("Success") : BadRequest("Fail");
    }
}