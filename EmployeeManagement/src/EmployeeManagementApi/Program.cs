using System.Text;
using Microsoft.OpenApi.Models;
using EmployeeManagement.Domain.Interfaces.Repositories;
using EmployeeManagement.Domain.Services;
using EmployeeManagement.Infrastructure.Repository;
using EmployeeManagement.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using EmployeeManagement.Application.Interfaces.Services;
using EmployeeManagement.Application.Services;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using BCrypt.Net;
using Microsoft.AspNetCore.DataProtection;

var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<Program>();
try
{
    logger.LogInformation("Iniciando a aplicação...");

    var builder = WebApplication.CreateBuilder(args);

    logger.LogInformation("Carregando configurações...");

    // 🔹 Configuração do banco de dados
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
            ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection")));
    });

    logger.LogInformation("Banco de dados configurado.");

    builder.Services.AddHttpContextAccessor();

    // 🔹 Configuração de repositórios e serviços
    builder.Services.AddScoped<IEmployeeService, EmployeeService>();
    builder.Services.AddScoped<IAuthService, AuthService>();  // Injetar AuthService

    builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
    builder.Services.AddScoped<IPhoneRepository, PhoneRepository>();

    // 🔹 Configuração de autenticação JWT
    var jwtKey = builder.Configuration["Jwt:Key"];
    var jwtEcrypt = builder.Configuration["Jwt:encrypt"];
    var jwtIssuer = builder.Configuration["Jwt:Issuer"];
    var jwtAudience = builder.Configuration["Jwt:Audience"];

    if (string.IsNullOrEmpty(builder.Configuration["Jwt:Key"]) || string.IsNullOrEmpty(jwtIssuer))
    {
        logger.LogError("Configuração JWT ausente!");
        throw new Exception("Configuração de autenticação inválida.");
    }

    builder.Services.AddAuthentication(
        options =>
        {
            options.DefaultAuthenticateScheme = "JwtBearer";
            options.DefaultChallengeScheme = "JwtBearer";
        })
        .AddJwtBearer("JwtBearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
                TokenDecryptionKey = new SymmetricSecurityKey(Encoding.Default.GetBytes(jwtEcrypt))
            };
            options.Events = new JwtBearerEvents
            {
                OnAuthenticationFailed = context =>
                {
                    // Console.WriteLine("Token Invalido ..:. " + context.Exception.Message);
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {
                    // Console.WriteLine("Token válido ..:. " + context.SecurityToken);
                    return Task.CompletedTask;
                }
            };
        });

    logger.LogInformation("Autenticação JWT configurada.");

    builder.Services.AddAuthorization();

    logger.LogInformation("Authorization configurada.");
    // 🔹 Configuração do Swagger com JWT Auth
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Employee Management API", Version = "v1" });

        var securitySchema = new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Enter 'Bearer YOUR_TOKEN_HERE'",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }
        };

        c.AddSecurityDefinition("Bearer", securitySchema);
        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        { securitySchema, new string[] { } }
        });
    });

    logger.LogInformation("SwaggerGen configurado.");
    // 🔹 Configuração do CORS (Permitir requisições do frontend)
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    });

    logger.LogInformation("CORS configurado.");

    builder.Services.AddLogging(builder =>
    {
        builder.AddConsole(); // Log para o console
        builder.AddDebug();   // Log para o console de depuração
    });

    logger.LogInformation("Logging configurado.");

    builder.Services.AddControllers();

    logger.LogInformation("Controllers configurado.");

    var app = builder.Build();

    logger.LogInformation("Iniciando build...");

    // migrate any database changes on startup (includes initial db creation)
    using (var scope = app.Services.CreateScope())
    {
        var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        dataContext.Database.Migrate();
    }
    logger.LogInformation("Migration configurada");

    // Middleware
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors("AllowAllOrigins");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    logger.LogInformation("Aplicação iniciada com sucesso!");

    app.Run();
}
catch (Exception ex)
{
    logger.LogCritical(ex, "Erro fatal ao iniciar a aplicação!");
}