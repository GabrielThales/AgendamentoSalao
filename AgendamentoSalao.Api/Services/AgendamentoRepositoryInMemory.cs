using System;
using System.Collections.Generic;
using System.Linq;
using AgendamentoSalao.Api.Models;

namespace AgendamentoSalao.Api.Services
{
    public class AgendamentoRepositoryInMemory : IAgendamentoRepository
    {
        // Uma lista estática funciona como um banco de dados enquanto a API estiver ligada
        private static readonly List<Agendamento> _agendamentos = new();

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
            return _agendamentos;
        }
    }
}