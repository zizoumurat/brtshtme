using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Pagination;
using BritishTime.Domain.Repositories.ClassRooms;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BritishTime.Infrastructure.Repositories.ClassRooms;

public class QueryClassRoomRepository : IQueryClassRoomRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryClassRoomRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<ClassRoomDto>> GetAllAsync(ClassRoomFilterDto filter, PageRequest pagination)
    {
        pagination ??= new PageRequest();

        var query = _context.Set<ClassRoom>().AsNoTracking();

        var count = await query.CountAsync();

        var items = await query
            .MultiSort(pagination.sortByMultiName, pagination.sortByMultiOrder)
            .Skip((pagination.Page - 1) * pagination.PageSize)
            .Take(pagination.PageSize)
            .ProjectTo<ClassRoomDto>(_mapper.ConfigurationProvider)
            .ToListAsync();

        return new PaginatedList<ClassRoomDto>(items, count, pagination.Page, pagination.PageSize);
    }

    public async Task<ClassRoomDto> GetByIdAsync(Guid id)
    {
        var classRoom = await _context.ClassRooms
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<ClassRoomDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return classRoom;
    }

    public async Task<bool> IsExistingAsync(Expression<Func<ClassRoom, bool>> predicate)
    {
        return await _context.Set<ClassRoom>().AnyAsync(predicate);
    }
}

