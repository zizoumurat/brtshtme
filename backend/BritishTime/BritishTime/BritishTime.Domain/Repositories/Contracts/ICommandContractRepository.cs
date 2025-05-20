using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.Contracts;

public interface ICommandContractRepository
{
    Task AddAsync(Contract Contract);
    Task UpdateAsync(Contract Contract);
    Task DeleteAsync(Contract Contract);
}