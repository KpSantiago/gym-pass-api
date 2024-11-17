using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GymPass.Infrastructure.Authentication;

public static class AuthenticationConfiguration
{
    public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration) 
    {
        AuthenticationBuilder authenticationBuilder = services.AddAuthentication(s =>
        {
            s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });

        authenticationBuilder.ConfigureJwtBearer(configuration:  configuration);
    }

    private static void ConfigureJwtBearer(this AuthenticationBuilder authenticationBuilder, IConfiguration configuration)
    {
        string? secret = configuration.GetSection("JwtSettings:Secret").Value;

        if (string.IsNullOrWhiteSpace(secret))
        {
            throw new ApplicationException("The secret string is missing. Verify application settings.");
        }
        
        byte[] key = Encoding.ASCII.GetBytes(secret);
        authenticationBuilder.AddJwtBearer(jwt =>
        {
            jwt.RequireHttpsMetadata = false;
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
    }
}