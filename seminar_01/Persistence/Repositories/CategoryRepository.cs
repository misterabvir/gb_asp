using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Base;
using Persistence.Contexts;
using Persistence.Errors;
using Persistence.Repositories.Abstractions;

namespace Persistence.Repositories;

internal class CategoryRepository : ICategoryRepository
{
    private readonly StoreContext _context;
    private readonly ILogger _logger;

    public CategoryRepository(StoreContext context, ILogger<CategoryRepository> logger)
    {
        _context = context;
       _logger = logger;
    }

    public async Task<Result<IEnumerable<Category>, Error>> Get()
    {
        try
        {
            _logger.LogInformation("Get all categories");
            return await _context
                .Categories
                .Include(c => c.Products)
                .AsNoTracking()
                .ToListAsync();

        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Category.Repository.GetAll", exception.Message);
        }
    }

    public async Task<Result<Category, Error>> Get(int id)
    {
        try
        {
            var category = await _context
                .Categories
                .Include(c => c.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
            if (category is null)
            {
                _logger.LogWarning("Category not found");
                return new NotFound("Category.Repository.GetById", "Category not found");
            }
            _logger.LogInformation($"Get Category \"{category.Name}\"");
            return category;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Category.Repository.GetById", exception.Message);
        }
    }

    public async Task<Result<Category, Error>> Create(Category entity)
    {
        try
        {
            var category = await _context
                .Categories
                .Include(c => c.Products)
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name == entity.Name);
            if (category is not null)
            {
                _logger.LogWarning("Category already exists");
                return new AlreadyExist("Category.Repository.Create", $"Category with name {entity.Name} already exists");
            }

            category = (await _context.Categories.AddAsync(entity)).Entity;
            _logger.LogInformation($"Category \"{category.Name}\" created");
            return category;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Category.Repository.Create", exception.Message);
        }
    }

    public async Task<Result<Category, Error>> UpdateName(int id, string name)
    {

        try
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
            if(category is not null)
            {
                _logger.LogWarning("Category already exists");
                return new AlreadyExist("Category.Repository.UpdateName", $"Category with name {name} already exists");
            }

            category= await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(category is null)
            {
                _logger.LogWarning("Category not found");
                return new NotFound("Category.Repository.UpdateName", "Category not found");
            }

            category.Name = name;
            _logger.LogInformation("Category updated");
            return category;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Category.Repository", exception.Message);
        }
    }

    public async Task<Result<Category, Error>> Delete(int id)
    {
        try
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if (category is null)
            {
                _logger.LogWarning("Category not found");
                return new NotFound("Category.Repository.Delete", "Category not found");
            }

            _context.Categories.Remove(category);
            _logger.LogInformation($"Category \"{category.Name}\" deleted");
            return category;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, exception.Message);
            return new Problem("Category.Repository.Delete", exception.Message);
        }
    }
}
