using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.CrmRecordActions;

public interface ICommandCrmRecordActionRepository
{
    Task AddAsync(CrmRecordAction CrmRecordAction);
}