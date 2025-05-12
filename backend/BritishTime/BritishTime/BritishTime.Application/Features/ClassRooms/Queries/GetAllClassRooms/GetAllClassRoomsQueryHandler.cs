
using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.ClassRoomsFeatures.Queries.GetAllClassRooms;

public sealed class GetAllClassRoomsQueryHandler : IRequestHandler<GetAllClassRoomsQuery, GetAllClassRoomsQueryResponse>
{
    private readonly IClassRoomService _ClassRoomService;

    public GetAllClassRoomsQueryHandler(IClassRoomService ClassRoomService)
    {
        _ClassRoomService = ClassRoomService;
    }
    public async Task<GetAllClassRoomsQueryResponse> Handle(GetAllClassRoomsQuery request, CancellationToken cancellationToken)
    {
        var result = await _ClassRoomService.GetAllAsync(request.filter, request.pagination);

        return new(result);
    }
}