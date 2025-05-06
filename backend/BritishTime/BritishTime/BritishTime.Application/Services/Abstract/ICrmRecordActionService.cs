using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface ICrmRecordActionService
{
    Task<IList<CrmRecordActionDto>> GetListByCrmRecord(Guid CrmRecordId);
    Task<CrmRecordActionDto> AddAsync(CrmRecordActionCreateDto CrmRecordActionDto);
    Task<IList<AppointmentListDto>> GetValidAppointmentsByDateAsync(DateTime date);
    Task<IList<AppointmentListDto>> GetValidCallsByDateAsync(DateTime date);
}
