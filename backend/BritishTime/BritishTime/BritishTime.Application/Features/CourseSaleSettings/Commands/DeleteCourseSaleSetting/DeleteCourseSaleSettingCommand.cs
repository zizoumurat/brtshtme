using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.CourseSaleSettings.Commands.DeleteCourseSaleSetting;

public sealed record DeleteCourseSaleSettingCommand(Guid Id) : IRequest<DeleteCourseSaleSettingCommandResponse>;