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

app.MapPost("/agendamentos", (AgendamentoRequest request, AgendamentoService service) =>
{
    try
    {
        // Aqui usamos o serviço que você testou com TDD! 
        // Ele vai validar as regras de negócio antes de salvar.
        service.Agendar(request);
        return Results.Created("/agendamentos", request);
    }
    catch (ArgumentException ex) // Erro de data no passado
    {
        return Results.BadRequest(new { erro = ex.Message });
    }
    catch (InvalidOperationException ex) // Erro de conflito de horário
    {
        return Results.Conflict(new { erro = ex.Message });
    }
});

app.MapGet("/agendamentos", (IAgendamentoRepository repo) =>
{
    var agendamentos = repo.ObterTodos();
    return Results.Ok(agendamentos);
});


app.Run();