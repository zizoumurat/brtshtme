using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Branches.Commands.UpdateBranch;

public class UpdateBranchCommandHandler : IRequestHandler<UpdateBranchCommand, UpdateBranchCommandResponse>
{
    private readonly IBranchService _branchService;

    public UpdateBranchCommandHandler(IBranchService branchService)
    {
        _branchService = branchService;
    }

    public async Task<UpdateBranchCommandResponse> Handle(UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        await _branchService.UpdateAsync(request.Branch);
        return new("updated");
    }
}
