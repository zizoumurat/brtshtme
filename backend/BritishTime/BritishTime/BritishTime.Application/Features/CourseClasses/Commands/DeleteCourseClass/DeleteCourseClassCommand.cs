using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.CourseClasses.Commands.DeleteCourseClass;

public sealed record DeleteCourseClassCommand(Guid Id) : IRequest<DeleteCourseClassCommandResponse>;