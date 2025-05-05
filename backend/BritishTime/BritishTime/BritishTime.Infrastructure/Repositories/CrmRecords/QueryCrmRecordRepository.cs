using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
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

        var items = await query
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

