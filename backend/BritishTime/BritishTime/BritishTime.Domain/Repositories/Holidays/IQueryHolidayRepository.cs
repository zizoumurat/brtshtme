using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.Holidays;

public interface IQueryHolidayRepository
{
    Task<bool> IsExistingAsync(Expression<Func<Holiday, bool>> predicate);
    IQueryable<Holiday> GetList(Expression<Func<Holiday, bool>> expression, bool isTracking = false);
    Task<HolidayDto> GetByIdAsync(Guid id);
}
