using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.CourseSaleSettings.Commands.UpdateCourseSaleSetting;

public sealed record UpdateCourseSaleSettingCommand(CourseSaleSettingDto CourseSaleSetting) : IRequest<UpdateCourseSaleSettingCommandResponse>;