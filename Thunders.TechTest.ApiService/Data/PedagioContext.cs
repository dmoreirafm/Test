using Microsoft.EntityFrameworkCore;
using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.ApiService.Data
{
    public class PedagioContext : DbContext
    {
        public PedagioContext(DbContextOptions<PedagioContext> options) : base(options) { }

        public DbSet<UtilizacaoPedagio> Utilizacoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UtilizacaoPedagio>(entity =>
            {
                entity.HasIndex(e => new { e.Cidade, e.DataHora });
                entity.HasIndex(e => new { e.Praca, e.DataHora });
                entity.HasIndex(e => e.TipoVeiculo);
            });
        }
    }
}
