using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.Contracts;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.Contracts;

public class CommandContractRepository : ICommandContractRepository
{
    private readonly ApplicationDbContext _context;

    public CommandContractRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Contract Contract)
    {
        _context.Contracts.Add(Contract);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Contract Contract)
    {
        _context.Contracts.Update(Contract);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Contract Contract)
    {
        _context.Contracts.Remove(Contract);
        await _context.SaveChangesAsync();
    }
}

