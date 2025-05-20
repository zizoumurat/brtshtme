using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.Students;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BritishTime.Infrastructure.Repositories.Students;

public class QueryStudentRepository : IQueryStudentRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryStudentRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<StudentDto>> GetAllAsync(StudentFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<Student>().AsNoTracking();

        var count = await query.CountAsync();

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<StudentDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<StudentDto> GetByIdAsync(Guid id)
    {
        var Student = await _context.Students
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<StudentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return Student;
    }

    public async Task<bool> IsExistingAsync(Expression<Func<Student, bool>> predicate)
    {
        return await _context.Set<Student>().AnyAsync(predicate);
    }
}

