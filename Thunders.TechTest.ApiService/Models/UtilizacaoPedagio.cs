namespace Thunders.TechTest.ApiService.Models
{
    public class UtilizacaoPedagio
    {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public string Praca { get; set; } = string.Empty; // Inicialize a propriedade
        public string Cidade { get; set; } = string.Empty; // Inicialize a propriedade
        public string Estado { get; set; } = string.Empty; // Inicialize a propriedade
        public decimal Valor { get; set; }
        public TipoVeiculo TipoVeiculo { get; set; }
    }

    public enum TipoVeiculo
    {
        Moto,
        Carro,
        Caminhao
    }
}
