using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.TechTest.ApiService.Controllers;
using Thunders.TechTest.ApiService.Mensagens;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.Tests.Integration.Controllers
{
    public class UtilizacaoControllerTests
    {
        private readonly Mock<IMessageSender> _messageSenderMock;
        private readonly Mock<ILogger<UtilizacaoController>> _loggerMock;
        private readonly UtilizacaoController _controller;

        public UtilizacaoControllerTests()
        {
            _messageSenderMock = new Mock<IMessageSender>();
            _loggerMock = new Mock<ILogger<UtilizacaoController>>();
            _controller = new UtilizacaoController(_messageSenderMock.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task RegistrarUtilizacao_QuandoDadosValidos_DeveRetornarAccepted()
        {
            // Arrange
            var utilizacao = new UtilizacaoPedagio
            {
                DataHora = DateTime.Now,
                Praca = "TestePraca",
                Cidade = "TesteCidade",
                Estado = "SP",
                Valor = 10.00m,
                TipoVeiculo = TipoVeiculo.Carro
            };

            // Act
            var resultado = await _controller.RegistrarUtilizacao(utilizacao);

            // Assert
            var acceptedResult = Assert.IsType<AcceptedResult>(resultado);
            _messageSenderMock.Verify(m => m.SendLocal(
                It.Is<ProcessarUtilizacaoMessage>(msg => msg.Utilizacao == utilizacao)),
                Times.Once);
        }

        [Fact]
        public async Task RegistrarUtilizacao_QuandoOcorreErro_DeveRetornarProblem()
        {
            // Arrange
            var utilizacao = new UtilizacaoPedagio
            {
                DataHora = DateTime.Now,
                Praca = "TestePraca",
                Cidade = "TesteCidade",
                Estado = "SP",
                Valor = 10.00m,
                TipoVeiculo = TipoVeiculo.Carro
            };

            _messageSenderMock
                .Setup(m => m.SendLocal(It.IsAny<ProcessarUtilizacaoMessage>()))
                .ThrowsAsync(new Exception("Erro simulado"));

            // Act
            var resultado = await _controller.RegistrarUtilizacao(utilizacao);

            // Assert
            Assert.IsType<ObjectResult>(resultado);
            Assert.Equal(500, (resultado as ObjectResult)?.StatusCode);
        }
    }
}
