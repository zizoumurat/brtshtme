using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Regions;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.Infrastructure.Repositories.Regions;

public class QueryRegionRepository : IQueryRegionRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryRegionRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<RegionDto>> GetAllAsync(RegionFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<Region>().AsNoTracking().Where(x =>
            (string.IsNullOrWhiteSpace(filter.Search) ||
             x.Name.ToLower().Contains(filter.Search.ToLower()) ||
             x.Description.ToLower().Contains(filter.Search.ToLower())));

        var count = await query.CountAsync();

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<RegionDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<RegionDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<RegionDto> GetByIdAsync(Guid id)
    {
        var region = await _context.Regions
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<RegionDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return region;
    }

    public async Task<List<SelectListDto>> GetListAsync()
    {
        return await _context.Regions.AsNoTracking().Select(x => new SelectListDto(x.Id, x.Name)).ToListAsync();
    }
}

