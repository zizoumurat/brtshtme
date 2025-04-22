using BritishTime.Domain.Entities;
using System.Linq.Expressions;

namespace BritishTime.Domain.Repositories.LessonScheduleDefinitions;

public interface ICommandLessonScheduleDefinitionRepository
{
    Task AddAsync(LessonScheduleDefinition LessonScheduleDefinition);
    Task UpdateAsync(LessonScheduleDefinition LessonScheduleDefinition);
    Task DeleteAsync(LessonScheduleDefinition LessonScheduleDefinition);
}   