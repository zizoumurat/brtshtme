using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.Branches;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.Branches;

public class CommandBranchRepository : ICommandBranchRepository
{
    private readonly ApplicationDbContext _context;

    public CommandBranchRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Branch branch)
    {
        _context.Branches.Add(branch);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Branch branch)
    {
        _context.Branches.Update(branch);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Branch branch)
    {
        _context.Branches.Remove(branch);
        await _context.SaveChangesAsync();
    }
}

