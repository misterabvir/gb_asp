using StoreApplication.Stores.Commands.Create;
using StoreApplication.Stores.Commands.Delete;
using StoreApplication.Stores.Commands.UpdateIdentityNumber;
using StoreApplication.Stores.Queries.GetAll;
using StoreApplication.Stores.Queries.GetById;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using StoreContracts.Stores;

namespace StorePresentation.Controllers;

public class StoresController : BaseController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public StoresController(
        ISender sender,
        IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("get_all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _sender.Send(new StoresGetAllQuery());
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!.Select(_mapper.Map<StoreResponse>));
    }

    [HttpGet]
    [Route("get_by_id")]
    public async Task<IActionResult> GetById([FromQuery] StoreByIdRequest request)
    {
        var query = _mapper.Map<StoresGetByIdQuery>(request)!;
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<StoreResponse>(result.Value!));
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(StoreCreateRequest request)
    {
        var command = _mapper.Map<StoresCreateCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<StoreResponse>(result.Value!));
    }

    [HttpPut]
    [Route("update_identity")]
    public async Task<IActionResult> UpdateIdentityNumber(StoreUpdateIdentityNumberRequest request)
    {
        var command = _mapper.Map<StoresUpdateIdentityNumberCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<StoreResponse>(result.Value!));
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete(StoreDeleteRequest request)
    {
        var command = _mapper.Map<StoresDeleteCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(_mapper.Map<StoreResponse>(result.Value!));
    }
}
