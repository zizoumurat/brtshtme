using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.ClassRooms;

public interface ICommandClassRoomRepository
{
    Task AddAsync(ClassRoom ClassRoom);
    Task UpdateAsync(ClassRoom ClassRoom);
    Task DeleteAsync(ClassRoom ClassRoom);
}