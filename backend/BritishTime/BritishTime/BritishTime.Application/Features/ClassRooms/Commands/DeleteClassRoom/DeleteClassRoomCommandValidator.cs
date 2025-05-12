using FluentValidation;

namespace BritishTime.Application.Features.ClassRooms.Commands.DeleteClassRoom;
public class DeleteClassRoomCommandValidator : AbstractValidator<DeleteClassRoomCommand>
{
    public DeleteClassRoomCommandValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty().WithMessage("RequiredField");
    }
}
