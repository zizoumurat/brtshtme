using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.ClassRooms.Commands.DeleteClassRoom;

public sealed record DeleteClassRoomCommand(Guid Id) : IRequest<DeleteClassRoomCommandResponse>;