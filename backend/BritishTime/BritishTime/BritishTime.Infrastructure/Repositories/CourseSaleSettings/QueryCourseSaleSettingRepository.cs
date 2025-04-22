using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.CourseSaleSettings;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.Infrastructure.Repositories.CourseSaleSettings;

public class QueryCourseSaleSettingRepository : IQueryCourseSaleSettingRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryCourseSaleSettingRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CourseSaleSettingDto>> GetAllAsync(CourseSaleSettingFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<CourseSaleSetting>().AsNoTracking();

        var count = await query.CountAsync();

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<CourseSaleSettingDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<CourseSaleSettingDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<CourseSaleSettingDto> GetByIdAsync(Guid id)
    {
        var CourseSaleSetting = await _context.CourseSaleSettings
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<CourseSaleSettingDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return CourseSaleSetting;
    }
}

