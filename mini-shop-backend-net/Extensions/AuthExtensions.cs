using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using mini_shop_backend_net.Application.Common.Exceptions;

namespace mini_shop_backend_net.Extensions;

public static class AuthExtensions
{
    public static void AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),

                    RoleClaimType = "role",
                    NameClaimType = "sub"
                };
                options.MapInboundClaims = false;

                options.Events = new JwtBearerEvents
                {
                    OnChallenge = context =>
                    {
                        context.HandleResponse();

                        var response = new ErrorResponse
                        (
                            "UNAUTHORIZED",
                            "Не авторизован",
                            DateTime.UtcNow,
                            context.Request.Path
                        );

                        context.Response.StatusCode = 401;
                        context.Response.ContentType = "application/json";

                        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    },

                    OnForbidden = context =>
                    {
                        var response = new ErrorResponse(
                            "FORBIDDEN",
                            "Нет доступа",
                            DateTime.UtcNow,
                            context.Request.Path
                        );

                        context.Response.StatusCode = 403;
                        context.Response.ContentType = "application/json";

                        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                };
            });

        services.AddAuthorization();
    }
}