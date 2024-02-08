using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Categories.Cache;
using ProductApplication.Categories.Errors;
using ProductApplication.Categories.Responses;
using ProductPersistence.Contexts;
using ProductsApplication.Helpers;

namespace ProductApplication.Categories.Commands.Create;

public sealed class CategoriesCreateCommandHandler : ICommandHandler<CategoriesCreateCommand, Result<CategoryResultResponse, Error>>
{

    private readonly ProductContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;

    public CategoriesCreateCommandHandler(ProductContext context, IMapper mapper, ICacheService cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<CategoryResultResponse, Error>> Handle(CategoriesCreateCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Name == request.Name, cancellationToken);

        if (category is not null)
        {
            return CategoryErrors.AlreadyExist(request.Name);
        }

        category = _mapper.Map<Category>(request)!;

        await _context.Categories.AddAsync(category, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        await _cache.RemoveAsync(CategoryKeys.All(), cancellationToken);

        return _mapper.Map<CategoryResultResponse>(category)!;
    }
}
