using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.Holidays;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BritishTime.Infrastructure.Repositories.Holidays;

public class QueryHolidayRepository : IQueryHolidayRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryHolidayRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<HolidayDto> GetByIdAsync(Guid id)
    {
        var Holiday = await _context.Holidays
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<HolidayDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return Holiday;
    }

    public IQueryable<Holiday> GetList(Expression<Func<Holiday, bool>> expression, bool isTracking = false)
    {
        if (isTracking)
        {
            return _context.Holidays.Where(expression);
        }
        else
        {
            return _context.Holidays.AsNoTracking().Where(expression);
        }
    }

    public async Task<bool> IsExistingAsync(Expression<Func<Holiday, bool>> predicate)
    {
        return await _context.Set<Holiday>().AnyAsync(predicate);
    }
}

