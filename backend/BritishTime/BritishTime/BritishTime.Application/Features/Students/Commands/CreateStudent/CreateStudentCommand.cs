using BritishTime.Domain.Dtos;
using MediatR;

namespace BritishTime.Application.Features.Students.Commands.CreateStudent;

public sealed record CreateStudentCommand(CalculatePaymentDto CalculatePaymentRequest, StudentCreateDto StudentRequest) : IRequest<CreateStudentCommandResponse>;