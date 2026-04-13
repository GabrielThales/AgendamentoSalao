using System;
using Moq;
using NUnit.Framework;
using AgendamentoSalao.Api.Services;
using AgendamentoSalao.Api.Models; 

namespace AgendamentoSalao.Tests
{
    [TestFixture]
    public class AgendamentoServiceTests
    {
        private Mock<IAgendamentoRepository> _agendamentoRepositoryMock;
        private AgendamentoService _agendamentoService;

        [SetUp]
        public void Setup()
        {
            _agendamentoRepositoryMock = new Mock<IAgendamentoRepository>();
            _agendamentoService = new AgendamentoService(_agendamentoRepositoryMock.Object);
        }

        [Test]
        public void Deve_Retornar_Erro_Quando_DataHora_For_No_Passado()
        {
            // Arrange
            var dataPassada = DateTime.Now.AddDays(-1);
            var request = new AgendamentoRequest(ClienteId: 1, ProfissionalId: 2, DataHora: dataPassada);

            // Act & Assert
            var excecao = Assert.Throws<ArgumentException>(() => _agendamentoService.Agendar(request));

            Assert.That(excecao.Message, Is.EqualTo("Não é possível agendar um horário no passado."));
        }

        [Test]
        public void Nao_Deve_Permitir_Agendamento_Quando_Profissional_Ja_Estiver_Ocupado()
        {
            // Arrange
            var dataDesejada = DateTime.Now.AddDays(1); // Data válida (no futuro)
            var request = new AgendamentoRequest(ClienteId: 1, ProfissionalId: 2, DataHora: dataDesejada);

            // Simulando (Mockando) que o banco de dados encontrou um agendamento
            _agendamentoRepositoryMock
                .Setup(repo => repo.ExisteAgendamento(request.ProfissionalId, request.DataHora))
                .Returns(true);

            // Act & Assert
            var excecao = Assert.Throws<InvalidOperationException>(() => _agendamentoService.Agendar(request));

            Assert.That(excecao.Message, Is.EqualTo("Profissional já possui um agendamento neste horário."));

        }

        [Test]
        public void Deve_Criar_Agendamento_Com_Sucesso_Quando_Horario_Estiver_Disponivel()
        {
            // Arrange
            var dataDesejada = DateTime.Now.AddDays(1); // Data válida
            var request = new AgendamentoRequest(ClienteId: 1, ProfissionalId: 2, DataHora: dataDesejada);

            // Mock responde "false" (ou seja, a agenda do profissional está livre)
            _agendamentoRepositoryMock
                .Setup(repo => repo.ExisteAgendamento(request.ProfissionalId, request.DataHora))
                .Returns(false);

            // Act
            _agendamentoService.Agendar(request);

            // Assert
            // Verificamos se o método "Salvar" do repositório foi chamado exatamente 1 vez
            _agendamentoRepositoryMock.Verify(repo => repo.Salvar(It.IsAny<Agendamento>()), Times.Once);
        }
    }
}