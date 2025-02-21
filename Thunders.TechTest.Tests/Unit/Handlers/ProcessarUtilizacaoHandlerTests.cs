using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.TechTest.ApiService.Data;
using Thunders.TechTest.ApiService.Handlers;
using Thunders.TechTest.ApiService.Mensagens;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.Tests.Fixtures;

namespace Thunders.TechTest.Tests.Unit.Handlers
{
    public class ProcessarUtilizacaoHandlerTests
    {
        private readonly Mock<ILogger<ProcessarUtilizacaoHandler>> _loggerMock;
        private readonly PedagioContext _context;
        private readonly ProcessarUtilizacaoHandler _handler;

        public ProcessarUtilizacaoHandlerTests()
        {
            var fixture = new PedagioTestsFixture();
            _context = fixture.CriarContexto();
            _loggerMock = new Mock<ILogger<ProcessarUtilizacaoHandler>>();
            _handler = new ProcessarUtilizacaoHandler(_context, _loggerMock.Object);
        }

        [Fact]
        public async Task Handle_QuandoReceberMensagem_DeveProcessarUtilizacao()
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

            var mensagem = new ProcessarUtilizacaoMessage { Utilizacao = utilizacao };

            // Act
            await _handler.Handle(mensagem);

            // Assert
            var utilizacaoSalva = await _context.Utilizacoes.FirstOrDefaultAsync();
            Assert.NotNull(utilizacaoSalva);
            Assert.Equal(utilizacao.Praca, utilizacaoSalva.Praca);
            Assert.Equal(utilizacao.Valor, utilizacaoSalva.Valor);
        }
    }
}
