using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.Branches.Commands.DeleteBranch;

public class DeleteBranchCommandHandler : IRequestHandler<DeleteBranchCommand, DeleteBranchCommandResponse>
{
    private readonly IBranchService _branchService;

    public DeleteBranchCommandHandler(IBranchService branchService)
    {
        _branchService = branchService;
    }

    public async Task<DeleteBranchCommandResponse> Handle(DeleteBranchCommand request, CancellationToken cancellationToken)
    {
        await _branchService.DeleteAsync(request.Id);

        return new("deleted");
    }
}
