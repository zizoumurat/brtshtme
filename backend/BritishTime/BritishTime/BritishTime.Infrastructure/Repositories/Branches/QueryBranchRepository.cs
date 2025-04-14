using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Branches;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.Infrastructure.Repositories.Branches;

public class QueryBranchRepository : IQueryBranchRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryBranchRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BranchDto>> GetAllAsync(BranchFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<Branch>().AsNoTracking().Where(x =>
            (string.IsNullOrWhiteSpace(filter.Search) ||
             x.Name.ToLower().Contains(filter.Search.ToLower()) ||
             x.PhoneNumber.ToLower().Contains(filter.Search.ToLower()) ||
             x.Email.ToLower().Contains(filter.Search.ToLower()) ||
             x.Description.ToLower().Contains(filter.Search.ToLower()) ||
             x.Address.ToLower().Contains(filter.Search.ToLower())) &&
            (filter == null || string.IsNullOrEmpty(filter.Name) || x.Name.ToLower().Contains(filter.Address.ToLower())) &&
            (filter == null || string.IsNullOrEmpty(filter.Address) || x.Address.ToLower().Contains(filter.Address.ToLower()))
        );

        var count = await query.CountAsync();

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<BranchDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<BranchDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<BranchDto> GetByIdAsync(Guid id)
    {
        var branch = await _context.Branches
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<BranchDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return branch;
    }
}

