using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.CourseSaleSettings;

public interface ICommandCourseSaleSettingRepository
{
    Task AddAsync(CourseSaleSetting CourseSaleSetting);
    Task UpdateAsync(CourseSaleSetting CourseSaleSetting);
    Task DeleteAsync(CourseSaleSetting CourseSaleSetting);
}