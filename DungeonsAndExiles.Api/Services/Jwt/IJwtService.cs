using DotNetEnv;
using DungeonsAndExiles.Api.Models.Authentication;
using DungeonsAndExiles.Api.Models.Domain;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DungeonsAndExiles.Api.Services.Jwt
{
    public interface IJwtService
    {
        Task<TokenResult> GenerateToken(User user, Role role);
    }
}
