using BritishTime.Application.Services.Abstract;
using BritishTime.Application.Services.concrete;
using FluentValidation;

namespace BritishTime.Application.Features.LessonScheduleDefinitions.Commands.UpdateLessonScheduleDefinition;
public class UpdateLessonScheduleDefinitionCommandValidator : AbstractValidator<UpdateLessonScheduleDefinitionCommand>
{
    private readonly ILessonScheduleDefinitionService _lessonScheduleDefinitionService;

    public UpdateLessonScheduleDefinitionCommandValidator(ILessonScheduleDefinitionService lessonScheduleDefinitionService)
    {
        _lessonScheduleDefinitionService = lessonScheduleDefinitionService;

        RuleFor(p => p.LessonScheduleDefinition.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.Schedule).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.StartTime).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.DayHour).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.Days).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.EducationType).IsInEnum().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.ScheduleCategory).IsInEnum().WithMessage("RequiredField");
        RuleFor(p => p.LessonScheduleDefinition.StudentType).IsInEnum().WithMessage("RequiredField");

        RuleFor(x => x)
           .MustAsync(async (command, cancellationToken) =>
           {
               var exists = await _lessonScheduleDefinitionService.ExistAsync(command.LessonScheduleDefinition);
               return !exists;
           })
           .WithMessage("AlreadyExist");

        RuleFor(x => x)
           .MustAsync(async (command, cancellationToken) =>
           {
               var exists = await _lessonScheduleDefinitionService
                    .ExistSchedule(command.LessonScheduleDefinition.Schedule, command.LessonScheduleDefinition.BranchId, command.LessonScheduleDefinition.Id);
               return !exists;
           })
           .WithMessage("AlreadyExistName");
    }
}
