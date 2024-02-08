using ProductApplication.Products.Commands.Create;
using ProductApplication.Products.Commands.Delete;
using ProductApplication.Products.Commands.UpdateCategory;
using ProductApplication.Products.Commands.UpdateDescription;
using ProductApplication.Products.Commands.UpdateName;
using ProductApplication.Products.Commands.UpdatePrice;
using ProductApplication.Products.Queries.GetAll;
using ProductApplication.Products.Queries.GetByCategoryId;
using ProductApplication.Products.Queries.GetById;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using ProductContracts.Products;

namespace ProductPresentation.Controllers;

public class ProductController : BaseController
{
    private readonly ISender _sender;
    private readonly IMapper _mapper;

    public ProductController(ISender sender, IMapper mapper)
    {
        _sender = sender;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("products_catalog")]
    public async Task<FileContentResult> GetProducts()
    {
        const string textType = "text/csv";
        const string filename = "products.csv";

        var result = await _sender.Send(new ProductsGetAllQuery());
        if (result.IsFailure)
        {
            return File(Encoding.UTF8.GetBytes(string.Empty), textType, filename);
        }

        var products = _mapper.Map<IEnumerable<ProductResponse>>(result.Value);

        var builder = new StringBuilder();
        builder.AppendLine($"Id,Name,Price,Description,CategoryId");
        foreach (var product in products!)
        {
            builder.Append(product.Id.ToString() + ",");
            builder.Append(product.Name.ToString() + ",");
            builder.Append(product.Price.ToString() + ",");
            builder.Append(product.Description?.ToString() ?? string.Empty + ",");
            builder.AppendLine(product.CategoryId.ToString());
        }
        var content = Encoding.UTF8.GetBytes(builder.ToString());
        return File(content, textType, filename);
    }

    [HttpGet]
    [Route("get_all")]
    public async Task<IActionResult> GetAll()
    {
        var query = new ProductsGetAllQuery();
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var products = result.Value!.Select(_mapper.Map<ProductResponse>).ToList()!;
        return Ok(products);
    }

    [HttpGet]
    [Route("get_by_id")]
    public async Task<IActionResult> GetById([FromQuery] ProductGetByIdRequest request)
    {
        var query = _mapper.Map<ProductsGetByIdQuery>(request)!;
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var product = _mapper.Map<ProductResponse>(result.Value!);
        return Ok(product);
    }

    [HttpGet]
    [Route("get_by_category")]
    public async Task<IActionResult> GetAllByCategoryId([FromQuery] ProductGetByCategoryIdRequest request)
    {
        var query = _mapper.Map<ProductsGetByCategoryIdQuery>(request)!;
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var products = result.Value!.Select(_mapper.Map<ProductResponse>).ToList()!;
        return Ok(products);
    }

    [HttpGet]
    [Route("is_exist_check")]
    public async Task<IActionResult> IsExist([FromQuery] ProductGetByIdRequest request)
    {
        var query = _mapper.Map<ProductsGetByIdQuery>(request)!;
        var result = await _sender.Send(query);
        if (result.IsFailure)
        {
            return Ok(false);
        }
        return Ok(true);
    }

    [HttpPost]
    [Route("create")]
    public async Task<IActionResult> Create(ProductCreateRequest request)
    {
        var command = _mapper.Map<ProductsCreateCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var product = _mapper.Map<ProductResponse>(result.Value!);

        return Ok(product);
    }

    [HttpPut]
    [Route("update_name")]
    public async Task<IActionResult> UpdateName(ProductUpdateNameRequest request)
    {
        var command = _mapper.Map<ProductsUpdateNameCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var product = _mapper.Map<ProductResponse>(result.Value!);

        return Ok(product);
    }

    [HttpPut]
    [Route("update_description")]
    public async Task<IActionResult> UpdateDescription(ProductUpdateDescriptionRequest request)
    {
        var command = _mapper.Map<ProductsUpdateDescriptionCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var product = _mapper.Map<ProductResponse>(result.Value!);

        return Ok(product);
    }

    [HttpPut]
    [Route("update_price")]
    public async Task<IActionResult> UpdatePrice(ProductUpdatePriceRequest request)
    {
        var command = _mapper.Map<ProductsUpdatePriceCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var product = _mapper.Map<ProductResponse>(result.Value!);

        return Ok(product);
    }

    [HttpPut]
    [Route("update_category")]
    public async Task<IActionResult> UpdateCategory(ProductUpdateCategoryRequest request)
    {
        var command = _mapper.Map<ProductsUpdateCategoryCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var product = _mapper.Map<ProductResponse>(result.Value!);


        return Ok(product);
    }

    [HttpDelete]
    [Route("delete")]
    public async Task<IActionResult> Delete(ProductDeleteRequest request)
    {
        var command = _mapper.Map<ProductsDeleteCommand>(request)!;
        var result = await _sender.Send(command);
        if (result.IsFailure)
        {
            return ProblemResult(result.Errors);
        }
        var product = _mapper.Map<ProductResponse>(result.Value!);

        return Ok(product);
    }


}
