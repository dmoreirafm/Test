using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Data;

namespace Thunders.TechTest.ApiService.Services
{
    public class RelatorioService
    {
        private readonly PedagioContext _context;
        private readonly ILogger<RelatorioService> _logger;

        public RelatorioService(PedagioContext context, ILogger<RelatorioService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IResult> ObterValorTotalHoraCidade()
        {
            try
            {
                var resultado = await _context.Utilizacoes
                    .GroupBy(u => new { u.Cidade, Hora = u.DataHora.Hour })
                    .Select(g => new
                    {
                        Cidade = g.Key.Cidade,
                        Hora = g.Key.Hora,
                        ValorTotal = g.Sum(u => u.Valor)
                    })
                    .ToListAsync();

                return Results.Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar relatório por hora/cidade");
                return Results.Problem();
            }
        }

        public async Task<IResult> ObterPracasMaisFaturamento(int quantidade, int mes, int ano)
        {
            try
            {
                var resultado = await _context.Utilizacoes
                    .Where(u => u.DataHora.Month == mes && u.DataHora.Year == ano)
                    .GroupBy(u => u.Praca)
                    .Select(g => new
                    {
                        Praca = g.Key,
                        ValorTotal = g.Sum(u => u.Valor)
                    })
                    .OrderByDescending(x => x.ValorTotal)
                    .Take(quantidade)
                    .ToListAsync();

                return Results.Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar relatório de praças mais rentáveis");
                return Results.Problem();
            }
        }

        public async Task<IResult> ObterQuantidadeVeiculosPraca(string praca)
        {
            try
            {
                var resultado = await _context.Utilizacoes
                    .Where(u => u.Praca == praca)
                    .GroupBy(u => u.TipoVeiculo)
                    .Select(g => new
                    {
                        TipoVeiculo = g.Key,
                        Quantidade = g.Count()
                    })
                    .ToListAsync();

                return Results.Ok(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao gerar relatório de veículos por praça");
                return Results.Problem();
            }
        }
    }
}
