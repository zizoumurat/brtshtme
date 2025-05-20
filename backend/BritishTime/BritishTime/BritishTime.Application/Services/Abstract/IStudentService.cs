using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;
public interface IStudentService
{
    Task<PaginatedList<StudentDto>> GetAllAsync(StudentFilterDto filter, PageRequest pagination);
    Task<List<SelectListDto>> GetUserStudentListAsync();
    Task<StudentDto> AddAsync(CalculatePaymentDto CalculatePaymentRequest, StudentCreateDto StudentRequest);
    Task<StudentDto> UpdateAsync(StudentDto StudentDto);
    Task<bool> DeleteAsync(Guid id);
}
