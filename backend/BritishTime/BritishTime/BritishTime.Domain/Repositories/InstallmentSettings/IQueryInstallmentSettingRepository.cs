using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Domain.Repositories.InstallmentSettings;

public interface IQueryInstallmentSettingRepository
{
    Task<PaginatedList<InstallmentSettingDto>> GetAllAsync(InstallmentSettingFilterDto filter, PageRequest pagination);
    Task<InstallmentSettingDto> GetByIdAsync(Guid id);
}
