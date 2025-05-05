using BritishTime.Application.Services.Abstract;
using MediatR;

namespace BritishTime.Application.Features.AppUsers.Commands.CreateAppUser;

public class CreateAppUserCommandHandler : IRequestHandler<CreateAppUserCommand, CreateAppUserCommandResponse>
{
    private readonly IAppUserService _appUserService;

    public CreateAppUserCommandHandler(IAppUserService appUserService)
    {
        _appUserService = appUserService;
    }

    public async Task<CreateAppUserCommandResponse> Handle(CreateAppUserCommand request, CancellationToken cancellationToken)
    {
        await _appUserService.AddAsync(request.AppUser);
        return new("added");
    }
}
