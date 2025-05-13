using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.Employees;
using BritishTime.Infrastructure.Context;

namespace BritishTime.Infrastructure.Repositories.Employees;

public class CommandEmployeeRepository : ICommandEmployeeRepository
{
    private readonly ApplicationDbContext _context;

    public CommandEmployeeRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Employee Employee)
    {
        try
        {
            _context.Employees.Add(Employee);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {

            throw;
        }
    }

    public async Task UpdateAsync(Employee Employee)
    {
        _context.Employees.Update(Employee);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Employee Employee)
    {
        _context.Employees.Remove(Employee);
        await _context.SaveChangesAsync();
    }
}

