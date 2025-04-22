using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.LessonScheduleDefinitions;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BritishTime.Infrastructure.Repositories.LessonScheduleDefinitions;

public class QueryLessonScheduleDefinitionRepository : IQueryLessonScheduleDefinitionRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryLessonScheduleDefinitionRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<bool> ExistsAsync(Expression<Func<LessonScheduleDefinition, bool>> predicate)
    {
        return await _context.LessonScheduleDefinitions.AnyAsync(predicate);
    }

    public async Task<PaginatedList<LessonScheduleDefinitionDto>> GetAllAsync(LessonScheduleDefinitionFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<LessonScheduleDefinition>().AsNoTracking();

        var count = await query.CountAsync();

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<LessonScheduleDefinitionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<LessonScheduleDefinitionDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<LessonScheduleDefinitionDto> GetByIdAsync(Guid id)
    {
        var LessonScheduleDefinition = await _context.LessonScheduleDefinitions
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<LessonScheduleDefinitionDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return LessonScheduleDefinition;
    }
}

