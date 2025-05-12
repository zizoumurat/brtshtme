using FluentValidation;

namespace BritishTime.Application.Features.ClassRooms.Commands.CreateClassRoom;
public class CreateClassRoomCommandValidator : AbstractValidator<CreateClassRoomCommand>
{
    public CreateClassRoomCommandValidator()
    {
        RuleFor(p => p.ClassRoom.Name).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ClassRoom.Capacity).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ClassRoom.BranchId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
