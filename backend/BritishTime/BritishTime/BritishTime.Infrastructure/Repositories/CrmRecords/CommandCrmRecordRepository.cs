using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.CrmRecords;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.CrmRecords;

public class CommandCrmRecordRepository : ICommandCrmRecordRepository
{
    private readonly ApplicationDbContext _context;

    public CommandCrmRecordRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(CrmRecord CrmRecord)
    {
        _context.CrmRecords.Add(CrmRecord);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(CrmRecord CrmRecord)
    {
        _context.CrmRecords.Update(CrmRecord);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(CrmRecord CrmRecord)
    {
        _context.CrmRecords.Remove(CrmRecord);
        await _context.SaveChangesAsync();
    }
}

