using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductApplication.Abstractions;
using ProductApplication.Base;
using ProductApplication.Categories.Cache;
using ProductApplication.Categories.Errors;
using ProductApplication.Categories.Responses;
using ProductPersistence.Contexts;
using ProductsApplication.Helpers;

namespace ProductApplication.Categories.Commands.UpdateName;

public sealed class CategoriesUpdateNameCommandHandler : ICommandHandler<CategoriesUpdateNameCommand, Result<CategoryResultResponse, Error>>
{

    private readonly ProductContext _context;
    private readonly IMapper _mapper;
    private readonly ICacheService _cache;

    public CategoriesUpdateNameCommandHandler(ProductContext context, IMapper mapper, ICacheService cache)
    {
        _context = context;
        _mapper = mapper;
        _cache = cache;
    }

    public async Task<Result<CategoryResultResponse, Error>> Handle(CategoriesUpdateNameCommand request, CancellationToken cancellationToken)
    {
        var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == request.Name, cancellationToken);
        if (category is not null)
        {
            return CategoryErrors.AlreadyExist(request.Name);
        }

        category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        if (category is null)
        {
            return CategoryErrors.NotFound(request.Id);
        }

        category.Name = request.Name;
        await _context.SaveChangesAsync(cancellationToken);

        await _cache.RemoveAsync(CategoryKeys.Id(request.Id), cancellationToken);
        await _cache.RemoveAsync(CategoryKeys.All(), cancellationToken);

        return _mapper.Map<CategoryResultResponse>(category)!;
    }
}