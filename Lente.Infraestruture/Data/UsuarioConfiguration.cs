using Lente.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;



namespace Lente.Infraestruture.Data
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.NomeDeUsuario)
                   .IsRequired()
                   .HasMaxLength(50)
                   .HasColumnType("nvarchar(50)");

            builder.Property(u => u.Senha)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnType("nvarchar(100)");


            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100)
                   .HasColumnType("nvarchar(100)");

            builder.Property(u => u.Perfil)
                   .IsRequired()
                   .HasMaxLength(20)
                   .HasColumnType("nvarchar(20)");

        }
    }
}
    

