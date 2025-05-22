using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.Holidays;

public interface ICommandHolidayRepository
{
    Task AddAsync(Holiday Holiday);
    Task UpdateAsync(Holiday Holiday);
    Task DeleteAsync(Holiday Holiday);
}