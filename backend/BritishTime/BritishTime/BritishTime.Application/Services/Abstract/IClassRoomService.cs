using BritishTime.Domain.Dtos;
using BritishTime.Domain.Pagination;

namespace BritishTime.Application.Services.Abstract;

public interface IClassRoomService
{
    Task<PaginatedList<ClassRoomDto>> GetAllAsync(ClassRoomFilterDto filter, PageRequest pagination);
    Task<ClassRoomDto> AddAsync(ClassRoomCreateDto ClassRoomDto);
    Task<ClassRoomDto> UpdateAsync(ClassRoomDto ClassRoomDto);
    Task<bool> DeleteAsync(Guid id);
}
