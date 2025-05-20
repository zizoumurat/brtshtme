using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.Payments;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.Payments;

public class CommandPaymentRepository : ICommandPaymentRepository
{
    private readonly ApplicationDbContext _context;

    public CommandPaymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Payment Payment)
    {
        _context.Payments.Add(Payment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Payment Payment)
    {
        _context.Payments.Update(Payment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Payment Payment)
    {
        _context.Payments.Remove(Payment);
        await _context.SaveChangesAsync();
    }
}

