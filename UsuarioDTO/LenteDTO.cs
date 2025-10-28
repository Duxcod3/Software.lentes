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
        public double DiagonalMaior { get; set; }

        public double Ponte { get; set; } // Novo campo para ponte da lente

        public string Lado { get; set; } // "Esquerda" ou "Direita"
        public int UsuarioId { get; set; }


    }
}
