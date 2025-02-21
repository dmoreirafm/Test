using Microsoft.AspNetCore.Mvc;
using Thunders.TechTest.ApiService.Mensagens;
using Thunders.TechTest.ApiService.Models;
using Thunders.TechTest.OutOfBox.Queues;

namespace Thunders.TechTest.ApiService.Controllers
{
    [ApiController]
    [Route("api/utilizacoes")]
    public class UtilizacaoController : ControllerBase
    {
        private readonly IMessageSender _messageSender;
        private readonly ILogger<UtilizacaoController> _logger;

        public UtilizacaoController(IMessageSender messageSender, ILogger<UtilizacaoController> logger)
        {
            _messageSender = messageSender;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarUtilizacao(UtilizacaoPedagio utilizacao)
        {
            try
            {
                await _messageSender.SendLocal(new ProcessarUtilizacaoMessage
                {
                    Utilizacao = utilizacao
                });

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao registrar utilização");
                return Problem();
            }
        }
    }
}
