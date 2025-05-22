using AutoMapper;
using AutoMapper.QueryableExtensions;
using BritishTime.Domain.Dtos;
using BritishTime.Domain.Entities;
using BritishTime.Domain.Repositories.LessonSessionTemplates;
using BritishTime.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BritishTime.Infrastructure.Repositories.LessonSessionTemplates;

public class QueryLessonSessionTemplateRepository : IQueryLessonSessionTemplateRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public QueryLessonSessionTemplateRepository(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<LessonSessionTemplateDto> GetByIdAsync(Guid id)
    {
        var LessonSessionTemplate = await _context.LessonSessionTemplates
            .Where(x => x.Id == id)
            .AsNoTracking()
            .ProjectTo<LessonSessionTemplateDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();

        return LessonSessionTemplate;
    }

    public IQueryable<LessonSessionTemplate> GetList(Expression<Func<LessonSessionTemplate, bool>> expression, bool isTracking = false)
    {
        if (isTracking)
        {
            return _context.LessonSessionTemplates.Where(expression);
        }
        else
        {
            return _context.LessonSessionTemplates.AsNoTracking().Where(expression);
        }
    }

    public async Task<bool> IsExistingAsync(Expression<Func<LessonSessionTemplate, bool>> predicate)
    {
        return await _context.Set<LessonSessionTemplate>().AnyAsync(predicate);
    }
}

