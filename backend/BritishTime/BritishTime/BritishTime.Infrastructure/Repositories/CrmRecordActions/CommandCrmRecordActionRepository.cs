using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.CrmRecordActions;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.CrmRecordActions;

public class CommandCrmRecordActionRepository : ICommandCrmRecordActionRepository
{
    private readonly ApplicationDbContext _context;

    public CommandCrmRecordActionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CrmRecordAction CrmRecordAction)
    {
        _context.CrmRecordActions.Add(CrmRecordAction);
        await _context.SaveChangesAsync();
    }
}

