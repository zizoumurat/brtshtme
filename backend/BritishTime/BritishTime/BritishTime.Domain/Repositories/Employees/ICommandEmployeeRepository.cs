using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.Employees;

public interface ICommandEmployeeRepository
{
    Task AddAsync(Employee Employee);
    Task UpdateAsync(Employee Employee);
    Task DeleteAsync(Employee Employee);
}