using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Enums;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.CrmRecords;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BritishTime.Infrastructure.Repositories.CrmRecords;

public class QueryCrmRecordRepository : IQueryCrmRecordRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryCrmRecordRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CrmRecordDto>> GetAllAsync(CrmRecordFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<CrmRecord>().AsNoTracking();

        var count = await query.CountAsync();

        if (filter != null)
        {
            query = query.Where(x =>
                    (filter.StartDate == null || x.Date.Date >= filter.StartDate.Value.Date) &&
                    (filter.EndDate == null || x.Date.Date <= filter.EndDate.Value.Date) &&
                    (string.IsNullOrEmpty(filter.FirstName) || x.FirstName.ToLower().Contains(filter.FirstName.ToLower())) &&
                    (string.IsNullOrEmpty(filter.LastName) || x.LastName.ToLower().Contains(filter.LastName.ToLower())) &&
                    (filter.Status == null || x.Status == filter.Status) &&
                    (filter.DataProviderId == null || x.DataProviderId == filter.DataProviderId) &&
                    (filter.RegionId == null || x.RegionId == filter.RegionId)
                );
        }

        var items = await query
            .Include(x => x.DataProvider)
            .Include(x => x.Region)
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<CrmRecordDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<CrmRecordDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<CrmRecordDto> GetByIdAsync(Guid id)
    {
        var crmRecord = await _context.CrmRecords
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<CrmRecordDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return crmRecord;
    }

    public async Task<CrmRecordDto> GetByPhone(string phone)
    {
        var crmRecord = await _context.CrmRecords
             .Where(x => x.Phone == phone)
             .AsNoTracking()
             .ProjectTo<CrmRecordDto>(_mapper.ConfigurationProvider)
             .FirstOrDefaultAsync();

        return crmRecord;
    }

    public async Task<bool> IsExistingAsync(Expression<Func<CrmRecord, bool>> predicate)
    {
        return await _context.Set<CrmRecord>().AnyAsync(predicate);
    }
}

