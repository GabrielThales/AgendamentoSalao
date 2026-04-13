namespace AgendamentoSalao.Api.Services
{
    public interface IAgendamentoRepository
    {
        bool ExisteAgendamento(int profissionalId, DateTime dataHora);
    }
}
