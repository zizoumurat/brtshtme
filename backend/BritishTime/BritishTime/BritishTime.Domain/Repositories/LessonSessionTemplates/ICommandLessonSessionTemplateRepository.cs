using BritishTime.Domain.Entities;

namespace BritishTime.Domain.Repositories.LessonSessionTemplates;

public interface ICommandLessonSessionTemplateRepository
{
    Task AddAsync(LessonSessionTemplate LessonSessionTemplate);
    Task AddRangeAsync(List<LessonSessionTemplate> LessonSessionTemplates);
    Task UpdateAsync(LessonSessionTemplate LessonSessionTemplate);
    Task DeleteAsync(LessonSessionTemplate LessonSessionTemplate);
}