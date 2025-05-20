using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Payments;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BritishTime.Infrastructure.Repositories.Payments;

public class QueryPaymentRepository : IQueryPaymentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryPaymentRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<PaymentDto>> GetAllAsync(PaymentFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<Payment>().AsNoTracking();

        var count = await query.CountAsync();

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<PaymentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<PaymentDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<PaymentDto> GetByIdAsync(Guid id)
    {
        var Payment = await _context.Payments
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<PaymentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return Payment;
    }

    public async Task<bool> IsExistingAsync(Expression<Func<Payment, bool>> predicate)
    {
        return await _context.Set<Payment>().AnyAsync(predicate);
    }
}

