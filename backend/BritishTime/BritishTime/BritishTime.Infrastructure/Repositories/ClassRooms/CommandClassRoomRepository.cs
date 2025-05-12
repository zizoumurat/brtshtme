using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.ClassRooms;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.ClassRooms;

public class CommandClassRoomRepository : ICommandClassRoomRepository
{
    private readonly ApplicationDbContext _context;

    public CommandClassRoomRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(ClassRoom ClassRoom)
    {
        _context.ClassRooms.Add(ClassRoom);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ClassRoom ClassRoom)
    {
        _context.ClassRooms.Update(ClassRoom);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(ClassRoom ClassRoom)
    {
        _context.ClassRooms.Remove(ClassRoom);
        await _context.SaveChangesAsync();
    }
}

