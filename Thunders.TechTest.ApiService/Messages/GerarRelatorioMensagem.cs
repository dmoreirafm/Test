using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.ApiService.Mensagens
{
    public class GerarRelatorioMensagem
    {
        public TipoRelatorio Tipo { get; set; }
        public string Parametros { get; set; } = string.Empty; // Inicialize a propriedade
    }
}
