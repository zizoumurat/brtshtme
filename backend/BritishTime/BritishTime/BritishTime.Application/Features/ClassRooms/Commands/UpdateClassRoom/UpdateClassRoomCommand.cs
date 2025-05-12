using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.ClassRooms.Commands.UpdateClassRoom;

public sealed record UpdateClassRoomCommand(ClassRoomDto ClassRoom) : IRequest<UpdateClassRoomCommandResponse>;