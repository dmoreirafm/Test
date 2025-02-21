namespace Thunders.TechTest.ApiService.Models
{
    public class Relatorio
    {
        public int Id { get; set; }
        public TipoRelatorio Tipo { get; set; }
        public DateTime DataGeracao { get; set; }
        public string Conteudo { get; set; } = string.Empty;
    }

    public enum TipoRelatorio
    {
        ValorTotalHoraCidade,
        PracasMaisFaturamento,
        QuantidadeVeiculosPraca
    }
}
