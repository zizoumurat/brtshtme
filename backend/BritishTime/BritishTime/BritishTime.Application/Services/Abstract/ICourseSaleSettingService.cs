using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface ICourseSaleSettingService
{
    Task<PaginatedList<CourseSaleSettingDto>> GetAllAsync(CourseSaleSettingFilterDto filter, PageRequest pagination);
    Task<CourseSaleSettingDto> AddAsync(CourseSaleSettingCreateDto CourseSaleSettingDto);
    Task<CourseSaleSettingDto> UpdateAsync(CourseSaleSettingDto CourseSaleSettingDto);
    Task<bool> DeleteAsync(Guid id);
}
