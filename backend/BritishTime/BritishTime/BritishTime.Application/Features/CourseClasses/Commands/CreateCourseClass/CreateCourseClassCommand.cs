using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.CourseClasses.Commands.CreateCourseClass;

public sealed record CreateCourseClassCommand(CourseClassCreateDto CourseClass) : IRequest<CreateCourseClassCommandResponse>;