using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.CrmRecords;

public interface ICommandCrmRecordRepository
{
    Task AddAsync(CrmRecord CrmRecord);
    Task UpdateAsync(CrmRecord CrmRecord);
    Task DeleteAsync(CrmRecord CrmRecord);
}