using FluentValidation;

namespace BritishTime.Application.Features.LessonScheduleDefinitions.Commands.UpdateLessonScheduleDefinition;
public class UpdateLessonScheduleDefinitionCommandValidator : AbstractValidator<UpdateLessonScheduleDefinitionCommand>
{
    public UpdateLessonScheduleDefinitionCommandValidator()
    {
        RuleFor(p => p.LessonScheduleDefinition.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.ScheduleCode).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.EducationType).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.StartTime).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.DayCount).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.DayHour).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.Days).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.ScheduleCategory).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.StudentType).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
