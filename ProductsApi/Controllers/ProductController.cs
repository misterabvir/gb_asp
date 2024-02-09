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
    public async Task<bool> ExistingById([FromQuery] ProductIsExistByIdRequest request)
    {
        return await _productService.IsProductExist(request);
    }

    [HttpPost(template: "create")]
    public async Task<IResult> Create(ProductCreateRequest request)
    {
        return await _productService.CreateProduct(request);
    }

    [HttpPut(template: "update_name")]
    public async Task<IResult> UpdateName(ProductUpdateNameRequest request)
    {
        return await _productService.UpdateNameProduct(request);
    }

    [HttpPut(template: "update_description")]
    public async Task<IResult> UpdateDescription(ProductUpdateDescriptionRequest request)
    {
        return await _productService.UpdateDescriptionProduct(request);
    }


    [HttpPut(template: "update_price")]
    public async Task<IResult> UpdatePrice(ProductUpdatePriceRequest request)
    {
        return await _productService.UpdatePriceProduct(request);
    }

    [HttpPut(template: "update_category")]
    public async Task<IResult> UpdateCategory(ProductUpdateCategoryRequest request)
    {
        return await _productService.UpdateCategoryProduct(request);
    }


    [HttpDelete(template: "delete")]
    public async Task<IResult> Delete(ProductDeleteRequest request)
    {
        return await _productService.DeleteProduct(request);
    }
}
