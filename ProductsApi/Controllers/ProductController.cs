using Contracts.Products.Requests;
using Microsoft.AspNetCore.Mvc;
using ProductsApi.BusinessLogicalLayer.Services.Base;

namespace ProductsApi.Controllers;

[Route("product-api/products")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet(template:"get_all")]
    public async Task<IActionResult> GetAll()
    {
        var response = await _productService.GetProducts();
        return Ok(response);
    }

    [HttpGet(template: "get_by_category_id")]
    public async Task<IActionResult> GetByCategoryId([FromQuery] ProductGetByCategoryIdRequest  request)
    {
        var response = await _productService.GetProductByCategoryId(request);
        return Ok(response);
    }

    [HttpGet(template: "get_by_id")]
    public async Task<IActionResult> GetById([FromQuery] ProductGetByIdRequest request)
    {
        var response = await _productService.GetProductById(request);
        return Ok(response);
    }


    [HttpGet(template: "existing_by_id")]
    public async Task<IActionResult> ExistingById([FromQuery] ProductGetByIdRequest request)
    {
        var response = await _productService.GetProductById(request);
        return Ok(response is not null);
    }

    [HttpPost(template: "create")]
    public async Task<IActionResult> Create(ProductCreateRequest request)
    {
        var response = await _productService.CreateProduct(request);
        return Ok(response);
    }

    [HttpPut(template: "update_name")]
    public async Task<IActionResult> UpdateName(ProductUpdateNameRequest request)
    {
        var response = await _productService.UpdateNameProduct(request);
        return response ? Ok("Success") : BadRequest("Fail");
    }

    [HttpPut(template: "update_description")]
    public async Task<IActionResult> UpdateDescription(ProductUpdateDescriptionRequest request)
    {
        var response = await _productService.UpdateDescriptionProduct(request);
        return response ? Ok("Success") : BadRequest("Fail");
    }


    [HttpPut(template: "update_price")]
    public async Task<IActionResult> UpdatePrice(ProductUpdatePriceRequest request)
    {
        var response = await _productService.UpdatePriceProduct(request);
        return response ? Ok("Success") : BadRequest("Fail");
    }

    [HttpPut(template: "update_category")]
    public async Task<IActionResult> UpdateCategory(ProductUpdateCategoryRequest request)
    {
        var response = await _productService.UpdateCategoryProduct(request);
        return response ? Ok("Success") : BadRequest("Fail");
    }


    [HttpDelete(template: "delete")]
    public async Task<IActionResult> Delete(ProductDeleteRequest request)
    {
        var response = await _productService.DeleteProduct(request);
        return response ? Ok("Success") : BadRequest("Fail");
    }
}
