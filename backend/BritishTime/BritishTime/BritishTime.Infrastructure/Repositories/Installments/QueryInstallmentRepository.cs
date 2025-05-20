using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Installments;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BritishTime.Infrastructure.Repositories.Installments;

public class QueryInstallmentRepository : IQueryInstallmentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryInstallmentRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<InstallmentDto>> GetAllAsync(InstallmentFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<Installment>().AsNoTracking();

        var count = await query.CountAsync();

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<InstallmentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<InstallmentDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<InstallmentDto> GetByIdAsync(Guid id)
    {
        var Installment = await _context.Installments
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<InstallmentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return Installment;
    }

    public async Task<bool> IsExistingAsync(Expression<Func<Installment, bool>> predicate)
    {
        return await _context.Set<Installment>().AnyAsync(predicate);
    }
}

