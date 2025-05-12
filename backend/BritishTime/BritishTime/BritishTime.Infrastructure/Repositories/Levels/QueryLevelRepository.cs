using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Levels;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BritishTime.Infrastructure.Repositories.Levels;

public class QueryLevelRepository : IQueryLevelRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryLevelRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<LevelDto>> GetAllAsync(LevelFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<Level>().AsNoTracking();

        var count = await query.CountAsync();

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<LevelDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<LevelDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<LevelDto> GetByIdAsync(Guid id)
    {
        var Level = await _context.Levels
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<LevelDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return Level;
    }

    public async Task<bool> IsExistingAsync(Expression<Func<Level, bool>> predicate)
    {
        return await _context.Set<Level>().AnyAsync(predicate);
    }
}

