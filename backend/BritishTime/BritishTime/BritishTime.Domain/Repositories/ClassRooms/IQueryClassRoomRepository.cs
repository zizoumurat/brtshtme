using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.ClassRooms;

public interface IQueryClassRoomRepository
{
    Task<bool> IsExistingAsync(Expression<Func<ClassRoom, bool>> predicate);
    Task<PaginatedList<ClassRoomDto>> GetAllAsync(ClassRoomFilterDto filter, PageRequest pagination);
    Task<ClassRoomDto> GetByIdAsync(Guid id);
}
