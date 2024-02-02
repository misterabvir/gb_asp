using Contracts.Products;
using Microsoft.AspNetCore.Mvc;
using Persistence.Repositories.Abstractions;
using Presentation.Extensions;

namespace Presentation.Controllers;

public class ProductController: BaseController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;

    public ProductController(IUnitOfWork unitOfWork, IProductRepository productRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _productRepository.Get();
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!);
    }

    [HttpGet]
    [Route("by/{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _productRepository.Get(id);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!.ToResponse());
    }


    [HttpGet]
    [Route("all/by/category/{id}")]
    public async Task<IActionResult> GetAllByCategoryId(int id)
    {
        var result = await _productRepository.GetByCategory(id);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        return Ok(result.Value!);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(ProductCreateRequest request)
    {
        var result = await _productRepository.Create(request.ToEntity());
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(result.Value!.ToResponse());
    }

    [HttpPut]
    [Route("update/name")]
    public async Task<IActionResult> UpdateName(ProductUpdateNameRequest request)
    {
        var result = await _productRepository.UpdateName(request.Id, request.Name);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(result.Value!.ToResponse());
    }

    [HttpPut]
    [Route("update/description")]
    public async Task<IActionResult> UpdateDescription(ProductUpdateDescriptionRequest request)
    {
        var result = await _productRepository.UpdateDescription(request.Id, request.Description);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(result.Value!.ToResponse());
    }

    [HttpPut]
    [Route("update/price")]
    public async Task<IActionResult> UpdatePrice(ProductUpdatePriceRequest request)
    {
        var result = await _productRepository.UpdatePrice(request.Id, request.Price);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(result.Value!.ToResponse());
    }

    [HttpPut]
    [Route("update/category")]
    public async Task<IActionResult> UpdateCategory(ProductUpdateCategoryRequest request)
    {
        var result = await _productRepository.UpdateCategory(request.Id, request.CategoryId);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(result.Value!.ToResponse());
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete(ProductDeleteRequest request)
    {
        var result = await _productRepository.Delete(request.Id);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        await _unitOfWork.SaveChangesAsync();
        return Ok(result.Value!.ToResponse());
    }
}
