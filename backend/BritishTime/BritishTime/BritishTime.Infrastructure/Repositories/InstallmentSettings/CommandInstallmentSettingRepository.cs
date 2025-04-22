using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.InstallmentSettings;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.InstallmentSettings;

public class CommandInstallmentSettingRepository : ICommandInstallmentSettingRepository
{
    private readonly ApplicationDbContext _context;

    public CommandInstallmentSettingRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(InstallmentSetting InstallmentSetting)
    {
        _context.InstallmentSettings.Add(InstallmentSetting);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(InstallmentSetting InstallmentSetting)
    {
        _context.InstallmentSettings.Update(InstallmentSetting);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(InstallmentSetting InstallmentSetting)
    {
        _context.InstallmentSettings.Remove(InstallmentSetting);
        await _context.SaveChangesAsync();
    }
}

