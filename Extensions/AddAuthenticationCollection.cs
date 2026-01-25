using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Groupchat_Api.Extensions
{
    public static class AddAuthenticationCollection
    {
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration config)
        {
            var secret = config["Jwt:Key"]
            ?? throw new InvalidOperationException("JWT key missing.");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = config["Jwt:Issuer"],
                        ValidAudience = config["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey
                        (
                            Encoding.UTF8.GetBytes(secret)
                        )
                    };
                });
            return services;
        }
    }
}