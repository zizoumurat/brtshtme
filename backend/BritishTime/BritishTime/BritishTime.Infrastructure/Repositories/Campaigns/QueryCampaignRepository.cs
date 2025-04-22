using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Campaigns;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.Infrastructure.Repositories.Campaigns;

public class QueryCampaignRepository : IQueryCampaignRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryCampaignRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CampaignDto>> GetAllAsync(CampaignFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<Campaign>().AsNoTracking();

        var count = await query.CountAsync();

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<CampaignDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<CampaignDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<CampaignDto> GetByIdAsync(Guid id)
    {
        var Campaign = await _context.Campaigns
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<CampaignDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return Campaign;
    }
}

