using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.LessonSessions;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BritishTime.Infrastructure.Repositories.LessonSessions;

public class QueryLessonSessionRepository : IQueryLessonSessionRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryLessonSessionRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LessonSessionDto> GetByIdAsync(Guid id)
    {
        var LessonSession = await _context.LessonSessions
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<LessonSessionDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return LessonSession;
    }

    public IQueryable<LessonSession> GetList(Expression<Func<LessonSession, bool>> expression, bool isTracking = false)
    {
        if (isTracking)
        {
            return _context.LessonSessions.Where(expression);
        }
        else
        {
            return _context.LessonSessions.AsNoTracking().Where(expression);
        }
    }

    public async Task<bool> IsExistingAsync(Expression<Func<LessonSession, bool>> predicate)
    {
        return await _context.Set<LessonSession>().AnyAsync(predicate);
    }
}

