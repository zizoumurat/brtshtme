using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.CourseClasses;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BritishTime.Infrastructure.Repositories.CourseClasses;

public class QueryCourseClassRepository : IQueryCourseClassRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryCourseClassRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CourseClassDto>> GetAllAsync(CourseClassFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<CourseClass>().AsNoTracking();

        var count = await query.CountAsync();

        var items = await query
            .Include(x=>x.Branch)
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<CourseClassDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<CourseClassDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<CourseClassDto> GetByIdAsync(Guid id)
    {
        var CourseClass = await _context.CourseClasses
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<CourseClassDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return CourseClass;
    }

    public async Task<bool> IsExistingAsync(Expression<Func<CourseClass, bool>> predicate)
    {
        return await _context.Set<CourseClass>().AnyAsync(predicate);
    }

    public IQueryable<CourseClass> GetList(Expression<Func<CourseClass, bool>> expression, bool isTracking = false)
    {
        if (isTracking)
        {
            return _context.CourseClasses.Where(expression);
        }
        else
        {
            return _context.CourseClasses.AsNoTracking().Where(expression);
        }
    }
}

