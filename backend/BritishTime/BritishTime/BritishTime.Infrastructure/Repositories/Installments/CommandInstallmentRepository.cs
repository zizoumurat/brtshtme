using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.Installments;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.Installments;

public class CommandInstallmentRepository : ICommandInstallmentRepository
{
    private readonly ApplicationDbContext _context;

    public CommandInstallmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Installment Installment)
    {
        _context.Installments.Add(Installment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Installment Installment)
    {
        _context.Installments.Update(Installment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Installment Installment)
    {
        _context.Installments.Remove(Installment);
        await _context.SaveChangesAsync();
    }
}

