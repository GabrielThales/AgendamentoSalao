namespace AgendamentoSalao.Api.Models
{
    public record AgendamentoRequest(int ClienteId, int ProfissionalId, DateTime DataHora);
}