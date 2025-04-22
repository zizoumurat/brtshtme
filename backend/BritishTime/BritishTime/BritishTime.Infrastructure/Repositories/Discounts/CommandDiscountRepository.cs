using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.Discounts;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.Discounts;

public class CommandDiscountRepository : ICommandDiscountRepository
{
    private readonly ApplicationDbContext _context;

    public CommandDiscountRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Discount Discount)
    {
        _context.Discounts.Add(Discount);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Discount Discount)
    {
        _context.Discounts.Update(Discount);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Discount Discount)
    {
        _context.Discounts.Remove(Discount);
        await _context.SaveChangesAsync();
    }
}

