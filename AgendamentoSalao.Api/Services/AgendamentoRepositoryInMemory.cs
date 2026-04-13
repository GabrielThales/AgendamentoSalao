using AgendamentoSalao.Api.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;


namespace AgendamentoSalao.Api.Services
{
    public class AgendamentoRepositoryInMemory : IAgendamentoRepository
    {
        [assembly: ExcludeFromCodeCoverage]
        // Uma lista estática funciona como um banco de dados enquanto a API estiver ligada
        private static readonly ConcurrentBag<Agendamento> _agendamentos = new();

        public bool ExisteAgendamento(int profissionalId, DateTime dataHora)
        {
            return _agendamentos.Any(a => a.ProfissionalId == profissionalId && a.DataHora == dataHora);
        }

        public void Salvar(Agendamento agendamento)
        {
            _agendamentos.Add(agendamento);
        }

        public IEnumerable<Agendamento> ObterTodos()
        {
            return _agendamentos.ToList();
        }
    }
}