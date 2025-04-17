using BritishTime.Application.Features.Auth.Login;
using BritishTime.Application.Services.Abstract;
using BritishTime.Domain.Entities;
using BritishTime.Infrastructure.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BritishTime.Infrastructure.Services;
internal class JwtProvider : IJwtProvider
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IOptions<JwtOptions> _jwtOptions;

    public JwtProvider(UserManager<AppUser> userManager, IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _jwtOptions = jwtOptions;
    }

    public async Task<LoginCommandResponse> CreateToken(AppUser user)
    {
        // Kullanıcıya ait claim'ler
        List<Claim> claims =
        [
            new Claim("Id", user.Id.ToString()),
            new Claim("Name", user.FullName),
            new Claim("Email", user.Email ?? ""),
            new Claim("UserName", user.UserName ?? ""),
        ];

        // Kullanıcıya ait roller
        var roles = await _userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        // Token süresi
        DateTime expires = DateTime.UtcNow.AddMonths(1);

        // Güvenlik anahtarı
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Value.SecretKey));

        // JWT token oluşturma
        var jwtSecurityToken = new JwtSecurityToken(
            issuer: _jwtOptions.Value.Issuer,
            audience: _jwtOptions.Value.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: expires,
            signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512));

        var handler = new JwtSecurityTokenHandler();
        string token = handler.WriteToken(jwtSecurityToken);

        // Refresh token ve süresi
        string refreshToken = Guid.NewGuid().ToString();
        DateTime refreshTokenExpires = expires.AddHours(1);

        // Refresh token bilgilerini güncelle
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = refreshTokenExpires;

        // Kullanıcıyı güncelle
        await _userManager.UpdateAsync(user);

        // Token ve refresh token döndür
        return new LoginCommandResponse(token, refreshToken, refreshTokenExpires);
    }
}

