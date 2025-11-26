using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lente.DTOs
{
    public class LenteDTO
    {
        public int Id { get; set; }
        public double Horizontal { get; set; }
        public double Vertical { get; set; }

        public double? DiagonalMaior { get; set; }

        public string? Fabricante { get; set; }

        public string? CodigoProduto { get; set; }

        public string? Modelo { get; set; }

        public string? Job { get; set; } // <-- Novo campo para o número da OS

        public double Ponte { get; set; } // Novo campo para ponte da lente

        public string Lado { get; set; } // "Esquerda" ou "Direita"
        public int UsuarioId { get; set; }


    }
}
