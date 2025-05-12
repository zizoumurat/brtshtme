using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.ClassRooms.Commands.DeleteClassRoom;

public class DeleteClassRoomCommandHandler : IRequestHandler<DeleteClassRoomCommand, DeleteClassRoomCommandResponse>
{
    private readonly IClassRoomService _ClassRoomService;

    public DeleteClassRoomCommandHandler(IClassRoomService ClassRoomService)
    {
        _ClassRoomService = ClassRoomService;
    }

    public async Task<DeleteClassRoomCommandResponse> Handle(DeleteClassRoomCommand request, CancellationToken cancellationToken)
    {
        await _ClassRoomService.DeleteAsync(request.Id);

        return new("deleted");
    }
}
