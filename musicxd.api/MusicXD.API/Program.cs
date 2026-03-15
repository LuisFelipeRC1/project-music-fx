using System.Text;
using MusicXD.API.Middleware;
using MusicXD.Application;
using MusicXD.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Validate required configuration at startup.
// Fail fast with a clear message rather than a cryptic runtime error later.
var requiredConfig = new[]
{
    ("Jwt:Key",                             "JWT signing key"),
    ("Jwt:Issuer",                          "JWT issuer"),
    ("Jwt:Audience",                        "JWT audience"),
    ("ConnectionStrings:DefaultConnection", "PostgreSQL connection string"),
    ("ConnectionStrings:Redis",             "Redis connection string"),
};

var configErrors = requiredConfig
    .Where(c => string.IsNullOrWhiteSpace(builder.Configuration[c.Item1]))
    .Select(c => $"  - {c.Item1} ({c.Item2})")
    .ToList();

if (configErrors.Count > 0)
{
    throw new InvalidOperationException(
        $"Missing required configuration values:\n{string.Join("\n", configErrors)}\n" +
        "Check your .env file or environment variables. See .env.example for reference.");
}

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]
                    ?? throw new InvalidOperationException("JWT key not configured.")))
        };
    });

builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MusicXD API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
            },
            Array.Empty<string>()
        }
    });
});

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
