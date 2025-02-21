using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.TechTest.ApiService.Models;

namespace Thunders.TechTest.Tests.Helpers
{
    public static class PedagioTestsHelper
    {
        public static List<UtilizacaoPedagio> GerarUtilizacoesParaTeste()
        {
            return new List<UtilizacaoPedagio>
            {
                new()
                {
                    DataHora = DateTime.Now.AddHours(-2),
                    Praca = "Praca1",
                    Cidade = "Cidade1",
                    Estado = "SP",
                    Valor = 10.50m,
                    TipoVeiculo = TipoVeiculo.Carro
                },
                new()
                {
                    DataHora = DateTime.Now.AddHours(-1),
                    Praca = "Praca1",
                    Cidade = "Cidade1",
                    Estado = "SP",
                    Valor = 15.75m,
                    TipoVeiculo = TipoVeiculo.Caminhao
                },
                new()
                {
                    DataHora = DateTime.Now,
                    Praca = "Praca2",
                    Cidade = "Cidade2",
                    Estado = "RJ",
                    Valor = 8.25m,
                    TipoVeiculo = TipoVeiculo.Moto
                }
            };
        }
    }
}
