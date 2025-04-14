using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.Branches;

public interface ICommandBranchRepository
{
    Task AddAsync(Branch branch);
    Task UpdateAsync(Branch branch);
    Task DeleteAsync(Branch branch);
}