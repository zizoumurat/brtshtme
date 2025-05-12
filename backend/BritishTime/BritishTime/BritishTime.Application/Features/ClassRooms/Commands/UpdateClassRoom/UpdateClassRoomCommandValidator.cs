using FluentValidation;

namespace BritishTime.Application.Features.ClassRooms.Commands.UpdateClassRoom;
public class UpdateClassRoomCommandValidator : AbstractValidator<UpdateClassRoomCommand>
{
    public UpdateClassRoomCommandValidator()
    {
        RuleFor(p => p.ClassRoom.Id).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ClassRoom.Name).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ClassRoom.Capacity).NotNull().NotEmpty().WithMessage("RequiredField");
        RuleFor(p => p.ClassRoom.BranchId).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
