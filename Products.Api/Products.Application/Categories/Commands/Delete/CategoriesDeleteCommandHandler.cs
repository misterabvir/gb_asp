using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Categories.Cache;
using ProductApplication.Categories.Errors;
using ProductApplication.Categories.Responses;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductPersistence.Contexts;
using ProductsApplication.Helpers;

namespace ProductApplication.Categories.Commands.Delete;

public sealed class CategoriesDeleteCommandHandler : ICommandHandler<CategoriesDeleteCommand, Result<CategoryResultResponse, Error>>
{
    private readonly ProductContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;

    public CategoriesDeleteCommandHandler(ProductContext context, IMapper mapper, ICacheService cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<CategoryResultResponse, Error>> Handle(CategoriesDeleteCommand request, CancellationToken cancellationToken)
    {

        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken: cancellationToken);
        if (category is null)
        {
            return CategoryErrors.NotFound(request.Id);
        }

        if(await _context.Products.AnyAsync(p => p.CategoryId == request.Id, cancellationToken: cancellationToken))
        {
            return CategoryErrors.HasProducts(category.Name);
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync(cancellationToken);

        await _cache.RemoveAsync(CategoryKeys.Id(request.Id), cancellationToken);
        await _cache.RemoveAsync(CategoryKeys.All(), cancellationToken);

        return _mapper.Map<CategoryResultResponse>(category)!;
    }
}

