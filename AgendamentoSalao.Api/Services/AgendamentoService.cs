using AgendamentoSalao.Api.Models;

namespace AgendamentoSalao.Api.Services
{
    public class AgendamentoService
    {
        private readonly IAgendamentoRepository _repository;

        public AgendamentoService(IAgendamentoRepository repository)
        {
            _repository = repository;
        }

        public void Agendar(AgendamentoRequest request)
        {
            if (request.DataHora < DateTime.Now)
            {
                throw new ArgumentException("Não é possível agendar um horário no passado.");
            }
        }
    }
}