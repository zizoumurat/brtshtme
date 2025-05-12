using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.ClassRooms.Commands.UpdateClassRoom;

public class UpdateClassRoomCommandHandler : IRequestHandler<UpdateClassRoomCommand, UpdateClassRoomCommandResponse>
{
    private readonly IClassRoomService _ClassRoomService;

    public UpdateClassRoomCommandHandler(IClassRoomService ClassRoomService)
    {
        _ClassRoomService = ClassRoomService;
    }

    public async Task<UpdateClassRoomCommandResponse> Handle(UpdateClassRoomCommand request, CancellationToken cancellationToken)
    {
        await _ClassRoomService.UpdateAsync(request.ClassRoom);
        return new("updated");
    }
}
