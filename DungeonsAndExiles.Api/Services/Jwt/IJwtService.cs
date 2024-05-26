using DotNetEnv;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DungeonsAndExiles.Api.Services.Jwt
{
    public interface IJwtService
    {
        public string GenerateToken(string userId);
    }
}
