using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Branches.Commands.CreateBranch;

public class CreateBranchCommandHandler : IRequestHandler<CreateBranchCommand, CreateBranchCommandResponse>
{
    private readonly IBranchService _branchService;

    public CreateBranchCommandHandler(IBranchService branchService)
    {
        _branchService = branchService;
    }

    public async Task<CreateBranchCommandResponse> Handle(CreateBranchCommand request, CancellationToken cancellationToken)
    {
        await _branchService.AddAsync(request.Branch);
        return new("added");
    }
}
