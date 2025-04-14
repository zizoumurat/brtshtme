using FluentValidation;

namespace BritishTime.Application.Features.LessonScheduleDefinitions.Commands.DeleteLessonScheduleDefinition;
public class DeleteLessonScheduleDefinitionCommandValidator : AbstractValidator<DeleteLessonScheduleDefinitionCommand>
{
    public DeleteLessonScheduleDefinitionCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
