using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.CrmRecordActions;

public interface IQueryCrmRecordActionRepository
{
    Task<PaginatedList<CrmRecordActionDto>> GetAllAsync(CrmRecordActionFilterDto filter, PageRequest pagination);
    Task<IList<CrmRecordActionDto>> GetListByCrmRecord(Guid crmRecordId);
    Task<IList<CrmRecordActionDto>> GetListAsync(Expression<Func<CrmRecordAction, bool>> predicate);
    Task<CrmRecordActionDto> GetByIdAsync(Guid id);
    Task<CrmRecordActionDto> GetAsync(Expression<Func<CrmRecordAction, bool>> predicate);
    Task<IList<AppointmentListDto>> GetValidAppointmentsByDateAsync(DateTime date, Guid employeeId);
    Task<IList<AppointmentListDto>> GetValidCallsByDateAsync(DateTime date, Guid employeeId);
    Task<IList<AppointmentListDto>> GetOpenAppointmentsAsync(Guid employeeId);
    Task<IList<AppointmentListDto>> GetOpenCallsAsync(Guid employeeId);

}

