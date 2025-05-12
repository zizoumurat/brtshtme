using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.ClassRooms.Commands.CreateClassRoom;

public sealed record CreateClassRoomCommand(ClassRoomCreateDto ClassRoom) : IRequest<CreateClassRoomCommandResponse>;