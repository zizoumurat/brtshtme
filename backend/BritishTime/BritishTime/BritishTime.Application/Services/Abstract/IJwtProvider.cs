using BritishTime.Application.Features.Auth.Login;
using BritishTime.Domain.Entities;

namespace BritishTime.Application.Services.Abstract;
public interface IJwtProvider
{
    Task<LoginCommandResponse> CreateToken(AppUser user);
}
