using AgendamentoSalao.Api.Models;
using AgendamentoSalao.Api.Services;
using System.Runtime.CompilerServices;

var builder = WebApplication.CreateBuilder(args);

// Adiciona suporte a exploração de endpoints (Swagger)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AgendamentoService>();

builder.Services.AddSingleton<IAgendamentoRepository, AgendamentoRepositoryInMemory>();
builder.Services.AddScoped<AgendamentoService>();

var app = builder.Build();

// Configura o Swagger (útil para testarmos quando o Docker subir)
app.UseSwagger();
app.UseSwaggerUI();

// Um endpoint simples de Healthcheck para provar que a API subiu
app.MapGet("/", () => "API do Salão de Beleza está rodando perfeitamente!");


app.Run();