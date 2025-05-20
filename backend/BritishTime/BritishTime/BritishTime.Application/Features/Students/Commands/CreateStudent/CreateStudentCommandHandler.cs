using BritishTime.Application.Services.Abstract;
using BritishTime.Application.Services.Concrete;
using MediatR;

namespace BritishTime.Application.Features.Students.Commands.CreateStudent;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, CreateStudentCommandResponse>
{
    private readonly IStudentService _studentService;

    public CreateStudentCommandHandler(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public async Task<CreateStudentCommandResponse> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        await _studentService.AddAsync(request.CalculatePaymentRequest, request.StudentRequest);

        return new("added");
    }
}
