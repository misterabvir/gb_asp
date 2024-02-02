using Presentation.Extensions;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories.Abstractions;
using Contracts.Categories;

namespace Presentation.Controllers;

public class CategoriesController : BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICategoryRepository _categoryRepository;

    public CategoriesController(IUnitOfWork unitOfWork, 
        ICategoryRepository categoryRepository) 
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    [Route("all")] 
    public async Task<IActionResult> GetAll()
    {
        var result = await _categoryRepository.Get();
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
        var result = await _categoryRepository.Get(id);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!.ToResponse());
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(CategoryCreateRequest request)
    {
        var result = await _categoryRepository.Create(request.ToEntity());
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(result.Value!.ToResponse());
    }

    [HttpPut]
    [Route("update/name/")]
    public async Task<IActionResult> UpdateName(CategoryUpdateNameRequest request)
    {
        var result = await _categoryRepository.UpdateName(request.Id, request.Name);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(result.Value!.ToResponse());
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete(CategoryDeleteRequest request)
    {
        var result = await _categoryRepository.Delete(request.Id);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return NoContent();
    }
}
