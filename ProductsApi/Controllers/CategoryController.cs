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
    public async Task<IActionResult> Create(CategoryCreateRequest request)
    {
        var response = await _categoryService.CreateCategory(request);
        return Ok(response);
    }

    [HttpPut(template: "update_name")]
    public async Task<IActionResult> UpdateName(CategoryUpdateNameRequest request)
    {
        var response = await _categoryService.UpdateNameCategory(request);
        return response ? Ok("Success") : BadRequest("Fail");
    }

    [HttpDelete(template: "delete")]
    public async Task<IActionResult> Delete(CategoryDeleteRequest request)
    {
        var response = await _categoryService.DeleteCategory(request);
        return response ? Ok("Success") : BadRequest("Fail");
    }
}