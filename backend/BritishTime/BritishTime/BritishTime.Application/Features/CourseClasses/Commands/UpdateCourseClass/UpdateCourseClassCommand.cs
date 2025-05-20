using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.CourseClasses.Commands.UpdateCourseClass;

public sealed record UpdateCourseClassCommand(CourseClassDto CourseClass) : IRequest<UpdateCourseClassCommandResponse>;