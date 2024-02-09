using Contracts.Categories.Requests;
using Microsoft.AspNetCore.Mvc;
using ProductsApi.BusinessLogicalLayer.Services.Base;

namespace ProductsApi.Controllers;

[Route("product-api/categories")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet(template:"get_all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _categoryService.GetCategories();
        return Ok(response);
    }

    [HttpGet(template: "get_by_id")]
    public async Task<IActionResult> GetById([FromQuery] CategoryGetByIdRequest request)
    {
        var response = await _categoryService.GetCategoryById(request);
        return Ok(response);
    }

    [HttpPost(template: "create")]
    public async Task<IResult> Create(CategoryCreateRequest request)
    {
        return await _categoryService.CreateCategory(request);
    }

    [HttpPut(template: "update_name")]
    public async Task<IResult> UpdateName(CategoryUpdateNameRequest request)
    {
        return await _categoryService.UpdateNameCategory(request);
    }

    [HttpDelete(template: "delete")]
    public async Task<IResult> Delete(CategoryDeleteRequest request)
    {
        return await _categoryService.DeleteCategory(request);
    }
}