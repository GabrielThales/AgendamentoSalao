using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using AgendamentoSalao.Api.Models;
using NUnit.Framework;

namespace AgendamentoSalao.Tests
{
    public class AgendamentoApiTests
    {
        private WebApplicationFactory<Program> _factory;
        private HttpClient _client;

        [SetUp]
        public void Setup()
        {
            // Isso sobe a API inteira na memória para os testes
            _factory = new WebApplicationFactory<Program>();
            _client = _factory.CreateClient();
        }

        [TearDown]
        public void TearDown()
        {
            _client.Dispose();
            _factory.Dispose();
        }

        [Test]
        public async Task POST_Deve_Retornar_Created_201_Quando_Agendamento_For_Valido()
        {
            // Arrange
            var request = new AgendamentoRequest(ClienteId: 1, ProfissionalId: 99, DataHora: DateTime.Now.AddDays(5));

            // Act
            var response = await _client.PostAsJsonAsync("/agendamentos", request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public async Task POST_Deve_Retornar_BadRequest_400_Quando_Data_For_No_Passado()
        {
            // Arrange
            var request = new AgendamentoRequest(ClienteId: 1, ProfissionalId: 100, DataHora: DateTime.Now.AddDays(-1));

            // Act
            var response = await _client.PostAsJsonAsync("/agendamentos", request);

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task GET_Deve_Retornar_Ok_200_E_Lista_De_Agendamentos()
        {
            // Act
            var response = await _client.GetAsync("/agendamentos");

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}