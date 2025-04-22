using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Discounts;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace BritishTime.Infrastructure.Repositories.Discounts;

public class QueryDiscountRepository : IQueryDiscountRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryDiscountRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<DiscountDto>> GetAllAsync(DiscountFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<Discount>().AsNoTracking();

        var count = await query.CountAsync();

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<DiscountDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<DiscountDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<DiscountDto> GetByIdAsync(Guid id)
    {
        var Discount = await _context.Discounts
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<DiscountDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return Discount;
    }
}

