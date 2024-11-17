using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GymPass.Domain.Authorization;
using GymPass.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GymPass.Infrastructure.Services;

public class TokenService : ITokenService
{
    private int _HOURS_TO_EXPIRE_TOKEN = 2;
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    // user.Roles.FirstOrDefault() != null  ? user.Roles.First().Role.Name
    
    public AccessToken GenerateToken(User user)
    {
        JwtSecurityTokenHandler handler = new();
        byte[] key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtSettings:Secret").Value!);
        
        SecurityTokenDescriptor tokenSpecificationDescriptor = DescribeTokenSpecification(user, key);
        SecurityToken securityToken = handler.CreateToken(tokenSpecificationDescriptor);
        string token = handler.WriteToken(securityToken);
        return new AccessToken(token, user.Name, "Cliente");
    }

    private SecurityTokenDescriptor DescribeTokenSpecification(User user, byte[] key)
    {
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = ConfigureClaimsIdentity(user),
            Expires = DateTime.UtcNow.AddHours(_HOURS_TO_EXPIRE_TOKEN),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        return tokenDescriptor;
    }

    private ClaimsIdentity ConfigureClaimsIdentity(User user)
    {
        ClaimsIdentity claimsIdentity = new ClaimsIdentity(new Claim[]
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Name, user.Name),
            new(ClaimTypes.Role, "Cliente"),
        });
        
        return claimsIdentity;
    }
}