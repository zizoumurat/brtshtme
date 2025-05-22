using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.CourseClasses.Commands.CreateLessionSession;

public sealed record CreateLessionSessionCommand(CreateLessonSessionDto request) : IRequest<CreateLessionSessionCommandResponse>;