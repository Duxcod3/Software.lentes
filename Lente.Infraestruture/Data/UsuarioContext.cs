using Lente.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;


namespace Lente.Infraestruture.Data
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<LenteModelo> Lentes { get; set; }       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Remove pluralização de nomes de tabelas    
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                modelBuilder.Entity(entity.ClrType).ToTable(entity.DisplayName());
            }
        

            // Desabilita delete em cascata para todos os relacionamentos
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }

            // Aplica configurações de todas as entidades 
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UsuarioContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}

