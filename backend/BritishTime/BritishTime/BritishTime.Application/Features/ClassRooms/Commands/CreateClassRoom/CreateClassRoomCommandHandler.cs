using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.ClassRooms.Commands.CreateClassRoom;

public class CreateClassRoomCommandHandler : IRequestHandler<CreateClassRoomCommand, CreateClassRoomCommandResponse>
{
    private readonly IClassRoomService _ClassRoomService;

    public CreateClassRoomCommandHandler(IClassRoomService ClassRoomService)
    {
        _ClassRoomService = ClassRoomService;
    }

    public async Task<CreateClassRoomCommandResponse> Handle(CreateClassRoomCommand request, CancellationToken cancellationToken)
    {
        await _ClassRoomService.AddAsync(request.ClassRoom);
        return new("added");
    }
}
