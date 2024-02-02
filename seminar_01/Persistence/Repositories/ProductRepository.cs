using Domain;
using Persistence.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Persistence.Contexts;
using Persistence.Errors;
using Persistence.Repositories.Abstractions;
using System.Diagnostics;

namespace Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly StoreContext _context;
    private readonly ILogger _logger;

    public ProductRepository(StoreContext context, ILogger<ProductRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<Result<IEnumerable<Product>, Error>> Get()
    {
        try
        {
            var products = await _context.Products
                    .Include(x => x.Stocks ?? new())
                    .ThenInclude(x => x.Store)
                    .Include(x => x.Category)
                    .AsNoTracking()
                    .ToListAsync();
            _logger.LogInformation("Products get all");
            return products;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Products.Repository.GetAll", exception.Message);
        }
    }

    public async Task<Result<Product, Error>> Get(int id)
    {
        try
        {
            var product = await _context.Products
                    .Include(x => x.Stocks ?? new())
                    .ThenInclude(x => x.Store)
                    .Include(x => x.Category)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);

            if (product is null)
            {
                return new NotFound("Products.Repositor.GetById", $"Product with id {id} not found");
            }

            _logger.LogInformation("Products get by id");
            return product;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Products.Repository.GetById", exception.Message);
        }
    }

    public async Task<Result<IEnumerable<Product>, Error>> GetByCategory(int? categoryId)
    {
        try
        {
            if (categoryId is not null)
            {
                var category = await _context
                    .Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == categoryId);

                if (category is null)
                {
                    _logger.LogWarning("Category not found");
                    return new NotFound("Products.Repository.GetByCategory", $"Category with id {categoryId} not found");
                }
            }
            var product = await _context.Products
                    .Include(x => x.Stocks ?? new())
                    .ThenInclude(x => x.Store)
                    .Include(x => x.Category)
                    .Where(x => x.CategoryId == categoryId)
                    .AsNoTracking()
                    .ToListAsync();

            _logger.LogInformation($"Products get by category with id {categoryId?.ToString() ?? "null"}");
            return product;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Products.Repository.GetByCategory", exception.Message);
        }
    }

    public async Task<Result<Product, Error>> Create(Product entity)
    {
        try
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == entity.CategoryId);
            if(category is null)
            {
                _logger.LogWarning($"Category with id {entity.CategoryId} not found");
                return new NotFound("Products.Repository.Create", $"Category with id {entity.CategoryId} not found");
            }
            
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == entity.Name);
            if(product is not null)
            {
                _logger.LogWarning($"Product with name {entity.Name} already exists");
                return new AlreadyExist("Products.Repository.Create", $"Product with name {entity.Name} already exists");
            }

            product = (await _context.Products.AddAsync(entity)).Entity;

            _logger.LogInformation($"Products with name {entity.Name} created");
            return product;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Products.Repository.Create", exception.Message);
        }
    }

    public async Task<Result<Product, Error>> UpdateCategory(int id, int? categoryId)
    {
        try
        {
            if (categoryId is not null)
            {
                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == categoryId);
                if (category is null)
                {
                    _logger.LogWarning($"Category with id {categoryId} not found");
                    return new NotFound("Products.Repository.UpdateName", $"Category with id {categoryId} not found");
                }
            }
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                _logger.LogWarning($"Product with id {id} already exists");
                return new NotFound("Products.Repository.UpdateName", $"Product with id {id} not found");
            }

            product.CategoryId = categoryId;

            _logger.LogInformation($"Product category with id {id} updated");
            return product;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Products.Repository.UpdateName", exception.Message);
        }
    }

    public async Task<Result<Product, Error>> UpdateDescription(int id, string description)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                _logger.LogWarning($"Product with id {id} not found");
                return new NotFound("Products.Repository.UpdateName", $"Product with id {id} not found");
            }

            product.Description = description;

            _logger.LogInformation($"Product description with id {id} updated");
            return product;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Products.Repository.UpdateName", exception.Message);
        }
    }

    public async Task<Result<Product, Error>> UpdateName(int id, string name)
    {
        try
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Name == name);
            if(product is not null)
            {
                _logger.LogWarning("Product with name already exists");
                return new AlreadyExist("Products.Repository.UpdateName", $"Product with name {name} already exists");
            }

            product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                _logger.LogWarning($"Product with id {id} not found");
                return new NotFound("Products.Repository.UpdateName", $"Product with id {id} not found");
            }

            product.Name = name;

            _logger.LogInformation($"Product name with id {id} updated");
            return product;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Products.Repository.UpdateName", exception.Message);
        }
    }

    public async Task<Result<Product, Error>> UpdatePrice(int id, decimal price)
    {
        try
        {
            if(price < 0)
            {
                _logger.LogWarning("Price must be greater than 0");
                return  new Problem("Products.Repository.UpdatePrice", "Price must be greater than 0");
            }

            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                _logger.LogWarning($"Product with id {id} not found");
                return new NotFound("Products.Repository.UpdatePrice", $"Product with id {id} not found");
            }

            product.Price = price;

            _logger.LogInformation($"Product price with id {id} updated");
            return product;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Products.Repository.UpdatePrice", exception.Message);
        }
    }

    public async Task<Result<Product, Error>> Delete(int id)
    {
        try
        {

            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (product is null)
            {
                _logger.LogWarning($"Product with id {id} not found");
                return new NotFound("Products.Repository.Delete", $"Product with id {id} not found");
            }

            _context.Products.Remove(product);

            _logger.LogInformation($"Product with id {id} deleted");
            return product;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Products.Repository.Delete", exception.Message);
        }
    }

}

