using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.InstallmentSettings;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.Infrastructure.Repositories.InstallmentSettings;

public class QueryInstallmentSettingRepository : IQueryInstallmentSettingRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryInstallmentSettingRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<InstallmentSettingDto>> GetAllAsync(InstallmentSettingFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<InstallmentSetting>().AsNoTracking();

        var count = await query.CountAsync();

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<InstallmentSettingDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<InstallmentSettingDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<InstallmentSettingDto> GetByIdAsync(Guid id)
    {
        var InstallmentSetting = await _context.InstallmentSettings
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<InstallmentSettingDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return InstallmentSetting;
    }
}

