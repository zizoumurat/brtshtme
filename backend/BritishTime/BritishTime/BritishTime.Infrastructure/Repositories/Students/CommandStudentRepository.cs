using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.Students;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.Students;

public class CommandStudentRepository : ICommandStudentRepository
{
    private readonly ApplicationDbContext _context;

    public CommandStudentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Student Student)
    {
        _context.Students.Add(Student);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Student Student)
    {
        _context.Students.Update(Student);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Student Student)
    {
        _context.Students.Remove(Student);
        await _context.SaveChangesAsync();
    }
}

