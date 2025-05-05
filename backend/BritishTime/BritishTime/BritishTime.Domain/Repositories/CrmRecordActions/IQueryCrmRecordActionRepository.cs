using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.CrmRecordActions;

public interface IQueryCrmRecordActionRepository
{
    Task<IList<CrmRecordActionDto>> GetListByCrmRecord(Guid crmRecordId);

    Task<IList<CrmRecordActionDto>> GetListAsync(Expression<Func<CrmRecordAction, bool>> predicate);

    Task<CrmRecordActionDto> GetByIdAsync(Guid id);

    Task<CrmRecordActionDto> GetAsync(Expression<Func<CrmRecordAction, bool>> predicate);
}

