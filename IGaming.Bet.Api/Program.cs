using IGaming.Bet.Solution.Extensions;
using Microsoft.AspNetCore.HttpOverrides;
using Service.Mapper;
using Shared.Config;

var builder = WebApplication.CreateBuilder(args);
var jwt = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();

builder.Services.ConfigureMySqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureIntegrationManager();
builder.Services.ConfigureServiceManager();

builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));
builder.Services.ConfigureJwtAuthentication(jwt!);
builder.Services.ConfigureJwtAuthorization();

builder.Services.ConfigureControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCustomSwaggerGen();

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.ConfigureApiVersioning();

var app = builder.Build();

app.Services.GetRequiredService<ILogger<Program>>();

app.ConfigureExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();