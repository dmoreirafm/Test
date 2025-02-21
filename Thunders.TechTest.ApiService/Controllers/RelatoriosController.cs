using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Services;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.Controllers
{
    [ApiController]
    [Route("api/relatorios")]
    public class RelatoriosController : ControllerBase
    {
        private readonly RelatorioService _relatorioService;

        public RelatoriosController(RelatorioService relatorioService)
        {
            _relatorioService = relatorioService;
        }

        [HttpGet("valor-total-hora-cidade")]
        public Task<IResult> ObterValorTotalHoraCidade()
        {
            return _relatorioService.ObterValorTotalHoraCidade();
        }

        [HttpGet("pracas-mais-faturamento")]
        public Task<IResult> ObterPracasMaisFaturamento(
            [FromQuery] int quantidade,
            [FromQuery] int mes,
            [FromQuery] int ano)
        {
            return _relatorioService.ObterPracasMaisFaturamento(quantidade, mes, ano);
        }

        [HttpGet("quantidade-veiculos-praca")]
        public Task<IResult> ObterQuantidadeVeiculosPraca([FromQuery] string praca)
        {
            return _relatorioService.ObterQuantidadeVeiculosPraca(praca);
        }
    }
}
