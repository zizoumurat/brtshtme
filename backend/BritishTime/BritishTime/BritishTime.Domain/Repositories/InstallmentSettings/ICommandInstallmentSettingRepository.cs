using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.InstallmentSettings;

public interface ICommandInstallmentSettingRepository
{
    Task AddAsync(InstallmentSetting InstallmentSetting);
    Task UpdateAsync(InstallmentSetting InstallmentSetting);
    Task DeleteAsync(InstallmentSetting InstallmentSetting);
}