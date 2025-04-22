using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.CourseSaleSettings.Commands.CreateCourseSaleSetting;

public sealed record CreateCourseSaleSettingCommand(CourseSaleSettingCreateDto CourseSaleSetting) : IRequest<CreateCourseSaleSettingCommandResponse>;