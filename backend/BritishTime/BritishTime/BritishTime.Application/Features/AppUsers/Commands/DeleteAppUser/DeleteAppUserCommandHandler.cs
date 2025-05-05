using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.AppUsers.Commands.DeleteAppUser;

public class DeleteAppUserCommandHandler : IRequestHandler<DeleteAppUserCommand, DeleteAppUserCommandResponse>
{
    private readonly IAppUserService _AppUserService;

    public DeleteAppUserCommandHandler(IAppUserService AppUserService)
    {
        _AppUserService = AppUserService;
    }

    public async Task<DeleteAppUserCommandResponse> Handle(DeleteAppUserCommand request, CancellationToken cancellationToken)
    {
        await _AppUserService.DeleteAsync(request.Id);

        return new("deleted");
    }
}
