using Lente.Application.Interface;  // Corrigido para manter apenas um "Interface"
using Lente.Application.Service;
using Lente.Domain.Entities;
using Lente.Domain.Interfaces;
using Lente.Infraestruture.Data;
using Lente.Infraestruture.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Software.lentes.DTOs;
using Software.lentes.Mapping;  // Referência para os profiles de mapeamento
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ---------------------
// Configuração do banco
// ---------------------
builder.Services.AddDbContext<UsuarioContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("MLENS"),
        sqlOptions => sqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,                     // Tenta até 5 vezes em caso de falha
            maxRetryDelay: TimeSpan.FromSeconds(10), // Intervalo de até 10s entre tentativas
            errorNumbersToAdd: null               // Usa erros padrão de falhas transitórias
        )
    )
);

// ---------------------
// CORS
// ---------------------
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// ---------------------
// Injeção de dependência
// ---------------------
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IJwtService, JwtServiceAdapter>();
builder.Services.AddScoped<ILenteRepository, LenteRepository>();  // Adicionado repositório para Lente
builder.Services.AddScoped<ILenteService, LenteService>();        // Adicionado serviço para Lente
builder.Services.AddAutoMapper(typeof(UsuarioProfile), typeof(LenteProfile));  // Registrando os profiles de AutoMapper
builder.Services.AddScoped<ILenteCalculadoraService, LenteCalculadoraService>();

// ---------------------
// Controllers
// ---------------------
builder.Services.AddControllers();

// ---------------------
// Configuração JWT
// ---------------------
var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ClockSkew = TimeSpan.Zero
    };
});

// ---------------------
// Swagger
// ---------------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ---------------------
// Build da aplicação
// ---------------------
var app = builder.Build();

// ---------------------
// Criação do usuário padrão
// ---------------------
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var usuarioService = services.GetRequiredService<IUsuarioService>();

    var usuariosExistentes = usuarioService.ListarUsuariosAsync().GetAwaiter().GetResult();
    if (!usuariosExistentes.Any())
    {
        var usuarioTeste = new Usuario
        {
            NomeDeUsuario = "admin",
            Email = "admin@teste.com",
            Perfil = "administrador",
        };

        string senhaTeste = "admin123";

        usuarioService.CriarUsuarioAsync(usuarioTeste, senhaTeste).GetAwaiter().GetResult();

        Console.WriteLine("Usuário padrão criado: admin / admin123");
    }
}

// ---------------------
// Pipeline HTTP
// ---------------------
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// CORS
app.UseCors("AllowAll");

// Autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

// Controllers
app.MapControllers();

// ---------------------
// Run
// ---------------------
app.Run();
