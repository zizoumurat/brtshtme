using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.CrmRecords;

public interface IQueryCrmRecordRepository
{
    Task<PaginatedList<CrmRecordDto>> GetAllAsync(CrmRecordFilterDto filter, PageRequest pagination);
    Task<bool> IsExistingAsync(Expression<Func<CrmRecord, bool>> predicate);
    Task<CrmRecordDto> GetByIdAsync(Guid id);
    Task<CrmRecordDto> GetByPhone(string phone);
}
