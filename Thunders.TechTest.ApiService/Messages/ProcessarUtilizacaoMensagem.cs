using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.ApiService.Mensagens
{
    public class ProcessarUtilizacaoMessage
    {
        public required UtilizacaoPedagio Utilizacao { get; set; }
    }
}
