using AgendamentoSalao.Api.Models;

namespace AgendamentoSalao.Api.Services
{
    public interface IAgendamentoRepository
    {
        bool ExisteAgendamento(int profissionalId, DateTime dataHora);
        void Salvar(Agendamento agendamento);
        IEnumerable<Agendamento> ObterTodos();
    }
}
