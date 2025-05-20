using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.Students;

public interface ICommandStudentRepository
{
    Task AddAsync(Student Student);
    Task UpdateAsync(Student Student);
    Task DeleteAsync(Student Student);
}