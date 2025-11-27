using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Lente.Infraestruture.Data
{
    public class UsuarioDbContextFactory : IDesignTimeDbContextFactory<UsuarioContext>
    {
        public UsuarioContext CreateDbContext(string[] args)
        {
            // Caminho correto para a pasta do projeto Software.lentes.Api onde está o appsettings.json
            var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "Software.lentes");

            Console.WriteLine("BasePath do appsettings.json: " + basePath);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("MLENS");

            Console.WriteLine("Connection string usada no factory: " + (connectionString ?? "NULL"));

            if (string.IsNullOrEmpty(connectionString))
                throw new InvalidOperationException("A string de conexão não foi encontrada no appsettings.json.");

            var optionsBuilder = new DbContextOptionsBuilder<UsuarioContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new UsuarioContext(optionsBuilder.Options);
        }
    }
}
