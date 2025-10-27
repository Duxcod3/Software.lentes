using Lente.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lente.Infraestruture.Data
{
    public class LenteConfiguration : IEntityTypeConfiguration<LenteModelo>
    {
        public void Configure(EntityTypeBuilder<LenteModelo> builder)
        {
            // Nome da tabela no banco de dados
            builder.ToTable("Lente");

            // Definindo a chave primária
            builder.HasKey(l => l.Id);

            // Mapeando as propriedades da entidade Lente
            builder.Property(l => l.Id)
                   .IsRequired();  // Definindo que o ID é requerido

            builder.Property(l => l.Horizontal)
                   .IsRequired()    // Definindo que Horizontal é requerido
                   .HasColumnType("decimal(18, 2)"); // Tipo de dado no banco, podendo ser ajustado conforme sua necessidade

            builder.Property(l => l.Vertical)
                   .IsRequired()
                   .HasColumnType("decimal(18, 2)");

            builder.Property(l => l.Diagonal)
                   .IsRequired()
                   .HasColumnType("decimal(18, 2)");

            // Configurações adicionais (exemplo para um campo opcional)
           // builder.Property(l => l.OutroCampo)
                //   .HasMaxLength(50)
                //   .HasColumnType("nvarchar(50)")
                 //  .IsRequired(false); // Caso seja opcional

            // Relacionamentos (quando houver)
            // Exemplo: builder.HasOne(l => l.Usuario).WithMany(u => u.Lentes).HasForeignKey(l => l.UsuarioId);
        }
    }
}
    

