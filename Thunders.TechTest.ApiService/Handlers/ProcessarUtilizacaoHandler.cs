using Rebus.Handlers;
using System.Diagnostics;
using Thunders.TechTest.ApiService.Data;
using Thunders.TechTest.ApiService.Mensagens;

namespace Thunders.TechTest.ApiService.Handlers
{
    public class ProcessarUtilizacaoHandler : IHandleMessages<ProcessarUtilizacaoMessage>
    {
        private readonly PedagioContext _context;
        private readonly ILogger<ProcessarUtilizacaoHandler> _logger;

        public ProcessarUtilizacaoHandler(PedagioContext context, ILogger<ProcessarUtilizacaoHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task Handle(ProcessarUtilizacaoMessage message)
        {
            try
            {
                await _context.Utilizacoes.AddAsync(message.Utilizacao);
                await _context.SaveChangesAsync();

                _logger.LogInformation(
                    "Utilização processada: Praça {praca}, Valor {valor}",
                    message.Utilizacao.Praca,
                    message.Utilizacao.Valor);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar utilização");
                throw;
            }
        }
    }
}
