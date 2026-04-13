using System;

namespace AgendamentoSalao.Api.Models
{
    public record Agendamento(int ClienteId, int ProfissionalId, DateTime DataHora);
}