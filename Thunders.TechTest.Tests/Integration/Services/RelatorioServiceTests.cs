using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.TechTest.ApiService.Services;
using Thunders.TechTest.Tests.Fixtures;
using Thunders.TechTest.Tests.Helpers;

namespace Thunders.TechTest.Tests.Integration.Services
{
    public class RelatorioServiceTests : IClassFixture<PedagioTestsFixture>
    {
        private readonly PedagioTestsFixture _fixture;
        private readonly ILogger<RelatorioService> _logger;

        public RelatorioServiceTests(PedagioTestsFixture fixture)
        {
            _fixture = fixture;
            _logger = Mock.Of<ILogger<RelatorioService>>();
        }

        [Fact]
        public async Task ObterValorTotalHoraCidade_QuandoExistemDados_DeveRetornarAgrupado()
        {
            // Arrange
            using var context = _fixture.CriarContexto();
            var service = new RelatorioService(context, _logger);

            var utilizacoes = PedagioTestsHelper.GerarUtilizacoesParaTeste();
            await context.Utilizacoes.AddRangeAsync(utilizacoes);
            await context.SaveChangesAsync();

            // Act
            var resultado = await service.ObterValorTotalHoraCidade();

            // Assert
            var okResult = Assert.IsType<Ok<object>>(resultado);
            var valores = Assert.IsAssignableFrom<IEnumerable<object>>(okResult.Value);
            Assert.NotEmpty(valores);
        }

        [Fact]
        public async Task ObterPracasMaisFaturamento_QuandoInformadaQuantidade_DeveRetornarLimitado()
        {
            // Arrange
            using var context = _fixture.CriarContexto();
            var service = new RelatorioService(context, _logger);

            var utilizacoes = PedagioTestsHelper.GerarUtilizacoesParaTeste();
            await context.Utilizacoes.AddRangeAsync(utilizacoes);
            await context.SaveChangesAsync();

            const int quantidadeEsperada = 2;

            // Act
            var resultado = await service.ObterPracasMaisFaturamento(quantidadeEsperada, DateTime.Now.Month, DateTime.Now.Year);

            // Assert
            var okResult = Assert.IsType<Ok<object>>(resultado);
            var pracas = Assert.IsAssignableFrom<IEnumerable<object>>(okResult.Value);
            Assert.True(pracas.Count() <= quantidadeEsperada);
        }

        [Fact]
        public async Task ObterQuantidadeVeiculosPraca_QuandoPracaExiste_DeveRetornarContagem()
        {
            // Arrange
            using var context = _fixture.CriarContexto();
            var relatorioService = new RelatorioService(context, _logger); // Instancie seu serviço com os parâmetros corretos
            var pracaId = "1"; // ID da praça que você está testando como string

            // Act
            var resultado = await relatorioService.ObterQuantidadeVeiculosPraca(pracaId); // Chame o método que está sendo testado

            // Assert
            var okResult = Assert.IsType<Ok<List<dynamic>>>(resultado); // Use 'dynamic' para aceitar o tipo anônimo
            var listaVeiculos = okResult.Value; // Acesse o valor retornado

            // Verifique se a lista não é nula e contém os dados esperados
            Assert.NotNull(listaVeiculos);
            Assert.NotEmpty(listaVeiculos); // Verifique se a lista não está vazia

            // Adicione mais asserções conforme necessário
            // Exemplo: Verificar se o tipo do primeiro item é o esperado
            var primeiroVeiculo = listaVeiculos[0];
            Assert.NotNull(primeiroVeiculo);
            Assert.True(primeiroVeiculo.GetType().GetProperty("TipoVeiculo") != null); // Verifique se a propriedade existe
            Assert.True(primeiroVeiculo.GetType().GetProperty("Quantidade") != null); // Verifique se a propriedade existe
        }
    }
}
