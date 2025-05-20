using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Contracts;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BritishTime.Infrastructure.Repositories.Contracts;

public class QueryContractRepository : IQueryContractRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryContractRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ContractDto>> GetAllAsync(ContractFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<Contract>().AsNoTracking();

        var count = await query.CountAsync();

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<ContractDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<ContractDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<ContractDto> GetByIdAsync(Guid id)
    {
        var Contract = await _context.Contracts
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<ContractDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return Contract;
    }

    public async Task<bool> IsExistingAsync(Expression<Func<Contract, bool>> predicate)
    {
        return await _context.Set<Contract>().AnyAsync(predicate);
    }
}

