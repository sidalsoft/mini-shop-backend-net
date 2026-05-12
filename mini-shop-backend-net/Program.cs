using System.Text.Encodings.Web;
using System.Text.Json;
using DotNetEnv;
using miniShopBackendNet.Extensions;
using miniShopBackendNet.Middleware;

Env.Load();

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

// Services
builder.Services.AddSwaggerDocumentation();
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddCorsConfiguration();
builder.Services.AddValidationServices();
builder.Services.AddApplicationServices();
builder.Services.AddRepositories();
builder.Services.AddDatabase(builder.Configuration);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy =
            JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Encoder =
            JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
    });

var app = builder.Build();

// Pipeline
app.UseSwaggerDocumentation();

app.UseCors("AllowFrontend");

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

await app.ApplyMigrationsAsync();
await app.SeedDatabaseAsync();

app.Run();