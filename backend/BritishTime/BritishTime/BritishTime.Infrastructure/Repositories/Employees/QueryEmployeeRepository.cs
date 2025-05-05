using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Employees;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BritishTime.Infrastructure.Repositories.Employees;

public class QueryEmployeeRepository : IQueryEmployeeRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryEmployeeRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<EmployeeDto>> GetAllAsync(EmployeeFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<Employee>().AsNoTracking();

        var count = await query.CountAsync();

        var items = await query
            .Include(x => x.Branch)
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<EmployeeDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public IQueryable<Employee> GetAllAsync(Expression<Func<Employee, bool>> expression, bool isTracking = false)
    {
        if (isTracking)
        {
            return _context.Employees.Where(expression);
        }
        else
        {
            return _context.Employees.AsNoTracking().Where(expression);
        }
    }

    public async Task<EmployeeDto> GetByIdAsync(Guid id)
    {
        var Employee = await _context.Employees
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<EmployeeDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return Employee;
    }

    public async Task<List<SelectListDto>> GetListAsync(Guid BranchId)
    {
        return await _context.Employees.Where(x => x.BranchId == BranchId).AsNoTracking()
            .Select(x => new SelectListDto(x.Id, $"{x.FirstName} {x.LastName}")).ToListAsync();
    }

    public async Task<bool> IsExistingAsync(Expression<Func<Employee, bool>> predicate)
    {
        return await _context.Set<Employee>().AnyAsync(predicate);
    }
}

