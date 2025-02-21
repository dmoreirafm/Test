using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Thunders.TechTest.ApiService.Data;

namespace Thunders.TechTest.Tests.Fixtures
{
    public class PedagioTestsFixture : IDisposable
    {
        public PedagioContext CriarContexto()
        {
            var options = new DbContextOptionsBuilder<PedagioContext>()
                .UseInMemoryDatabase(databaseName: $"PedagioTests_{Guid.NewGuid()}")
                .Options;

            var context = new PedagioContext(options);
            context.Database.EnsureCreated();
            return context;
        }

        public void Dispose()
        {
            // Cleanup se necessário
            //teste
        }
    }
}
