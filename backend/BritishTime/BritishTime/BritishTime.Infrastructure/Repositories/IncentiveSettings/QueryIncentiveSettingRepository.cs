using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.IncentiveSettings;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.Infrastructure.Repositories.IncentiveSettings;

public class QueryIncentiveSettingRepository : IQueryIncentiveSettingRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryIncentiveSettingRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<IncentiveSettingDto>> GetAllAsync(IncentiveSettingFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<IncentiveSetting>().AsNoTracking().Where(x =>
            (x.ParticipantType == filter.ParticipantType) &&
            (string.IsNullOrWhiteSpace(filter.Search)));

        var count = await query.CountAsync();

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<IncentiveSettingDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<IncentiveSettingDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<IncentiveSettingDto> GetByIdAsync(Guid id)
    {
        var IncentiveSetting = await _context.IncentiveSettings
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<IncentiveSettingDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return IncentiveSetting;
    }
}

