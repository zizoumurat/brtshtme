using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface ICrmRecordService
{
    Task<PaginatedList<CrmRecordDto>> GetAllAsync(CrmRecordFilterDto filter, PageRequest pagination);
    Task<CrmRecordDto> GetByIdAsync(Guid id);
    Task<CrmRecordDto> CheckPhone(string phone);
    Task<CrmRecordDto> AddAsync(CrmRecordCreateDto crmRecordDto);
    Task<CrmRecordDto> UpdateAsync(CrmRecordDto crmRecordDto);
    Task<bool> DeleteAsync(Guid id);
}
