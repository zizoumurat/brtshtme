using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Domain.Repositories.CourseSaleSettings;

public interface IQueryCourseSaleSettingRepository
{
    Task<PaginatedList<CourseSaleSettingDto>> GetAllAsync(CourseSaleSettingFilterDto filter, PageRequest pagination);
    Task<CourseSaleSettingDto> GetByIdAsync(Guid id);
}
