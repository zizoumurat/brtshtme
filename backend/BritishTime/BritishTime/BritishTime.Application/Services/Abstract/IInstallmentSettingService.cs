using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface IInstallmentSettingService
{
    Task<PaginatedList<InstallmentSettingDto>> GetAllAsync(InstallmentSettingFilterDto filter, PageRequest pagination);
    Task<InstallmentSettingDto> AddAsync(InstallmentSettingCreateDto InstallmentSettingDto);
    Task<InstallmentSettingDto> UpdateAsync(InstallmentSettingDto InstallmentSettingDto);
    Task<bool> DeleteAsync(Guid id);
}
